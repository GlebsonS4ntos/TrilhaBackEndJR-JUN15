using Desafio.API.Helpers;
using Desafio.API.Interfaces;
using Desafio.API.Models.Dtos;
using Desafio.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Desafio.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(ITokenService tokenService, JwtSettings jwtSettings, 
                                UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _tokenService = tokenService;
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromBody] RegisterDto register)
        {

            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user != null) 
            { 
                return Unauthorized(new { message = "Existing Email."});
            }

            User u = new User()
            {
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = register.Email
            };

            Console.WriteLine(u.Id);

            var result = await _userManager.CreateAsync(u, register.Password);

            if (!result.Succeeded) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Internal Server Error" });
            }

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user is not null && await _userManager.CheckPasswordAsync(user, login.Password)) 
            { 
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in userRoles)
                {
                    authClaims.Add(new
                        Claim(ClaimTypes.Role, role));
                }

                var token = _tokenService.GenerateToken(authClaims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.ExpireTimeRefreshToken = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenValidityInMinutes);

                await _userManager.UpdateAsync(user);
                
                return Ok(new
                {
                    TokenAcess = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    ExpiredToken = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] TokenDto token)
        {
            var principalClaims = _tokenService.GetPrincipalClaimsFromExpiredToken(token.TokenAcess);

            if (principalClaims == null)
            {
                return BadRequest("Invalid Token / Refresh Token");
            }

            var email = principalClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != token.RefreshToken || user.ExpireTimeRefreshToken < DateTime.UtcNow )
            {
                return BadRequest("Invalid Token / Refresh Token");
            }

            var newToken = _tokenService.GenerateToken(principalClaims.Claims.ToList());
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.ExpireTimeRefreshToken = DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenValidityInMinutes);

            await _userManager.UpdateAsync(user);

            return Ok(new
            {
                TokenAcess = new JwtSecurityTokenHandler().WriteToken(newToken),
                RefreshToken = newRefreshToken,
                ExpiredToken = newToken.ValidTo
            });
        }


        [HttpPost]
        [Route("revoke-token")]
        public async Task<IActionResult> RevokeToken([FromBody] TokenDto dto)
        {
            var principalClaims = _tokenService.GetPrincipalClaimsFromExpiredToken(dto.TokenAcess);

            if(principalClaims == null)
            {
                return BadRequest("Invalid Token / Refresh Token");
            }

            var email = principalClaims.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null || user.RefreshToken != dto.RefreshToken)
            {
                return BadRequest("Invalid Token / Refresh Token");
            }

            user.RefreshToken = null;
            user.ExpireTimeRefreshToken = DateTime.MinValue;

            await _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}