using Microsoft.AspNetCore.Identity;

namespace Desafio.API.Models.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime ExpireTimeRefreshToken { get; set; }
    }
}
