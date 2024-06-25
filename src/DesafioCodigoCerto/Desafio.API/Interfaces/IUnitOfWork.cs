namespace Desafio.API.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryEmployee RepositoryEmployee { get; }
        Task Commit();
    }
}
