using Desafio.API.Database.Context;
using Desafio.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CodigoCertoContext _context;

        public Repository(CodigoCertoContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}
