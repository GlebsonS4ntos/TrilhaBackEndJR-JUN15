using Desafio.API.Database.Context;
using Desafio.API.Interfaces;

namespace Desafio.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodigoCertoContext _context;
        private IRepositoryEmployee repositoryEmployee;
        private IRepositoryRevokedTokenAcess repositoryRevokedTokenAcess;

        public UnitOfWork(CodigoCertoContext context)
        {
            _context = context;
        }

        public IRepositoryEmployee RepositoryEmployee 
        {
            get
            {
                return repositoryEmployee = repositoryEmployee ?? new RepositoryEmployee(_context); 
            }
        }

        public IRepositoryRevokedTokenAcess RepositoryRevokedTokenAcess
        {
            get 
            {
                return repositoryRevokedTokenAcess = repositoryRevokedTokenAcess ?? new RepositoryRevokedTokenAcess(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
