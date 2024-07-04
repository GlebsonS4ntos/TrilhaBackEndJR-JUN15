using Desafio.API.Database.Context;
using Desafio.API.Interfaces;
using Desafio.API.Models.Entities;

namespace Desafio.API.Repositories
{
    public class RepositoryRevokedTokenAcess : IRepositoryRevokedTokenAcess
    {
        private readonly CodigoCertoContext _context;

        public RepositoryRevokedTokenAcess(CodigoCertoContext context)
        {
            _context = context;
        }

        public async Task AddRevokedToken(string token)
        {
            var revokedToken = new RevokedTokenAcess()
            {
                Id = Guid.NewGuid(),
                Token = token,
                CreatedAt = DateTime.UtcNow,
            };
            
            await _context.Set<RevokedTokenAcess>().AddAsync(revokedToken);
        }

        public bool IsTokenRevoked(string token)
        {
            var isRevoked = _context.Set<RevokedTokenAcess>().SingleOrDefault(t => t.Token == token);

            if (isRevoked == null) 
            {
                return false;
            }

            return true;
        }
    }
}
