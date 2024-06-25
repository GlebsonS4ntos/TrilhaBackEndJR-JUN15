using Desafio.API.Database.Context;
using Desafio.API.Interfaces;

namespace Desafio.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodigoCertoContext _context;
        private IRepositoryEmployee repositoryEmployee;

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

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
