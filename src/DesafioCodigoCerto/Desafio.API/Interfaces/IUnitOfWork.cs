namespace Desafio.API.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryRevokedTokenAcess RepositoryRevokedTokenAcess { get; }
        IRepositoryEmployee RepositoryEmployee { get; }
        Task Commit();
    }
}
