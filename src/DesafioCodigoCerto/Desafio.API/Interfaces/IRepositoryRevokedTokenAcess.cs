namespace Desafio.API.Interfaces
{
    public interface IRepositoryRevokedTokenAcess
    {
        public Task AddRevokedToken(string token);
        public bool IsTokenRevoked(string token);
    }
}