using Desafio.API.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Desafio.API.Database.Context
{
    public class CodigoCertoContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<RevokedTokenAcess> RevokedTokens { get; set; }

        public CodigoCertoContext(DbContextOptions<CodigoCertoContext> opt) : base(opt) { }
    }
}
