using Desafio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API.Database.Context
{
    public class CodigoCertoContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public CodigoCertoContext(DbContextOptions<CodigoCertoContext> opt) : base(opt) { }
    }
}
