using Desafio.API.Database.Context;
using Desafio.API.Interfaces;
using Desafio.API.Models;

namespace Desafio.API.Repositories
{
    public class RepositoryEmployee : Repository<Employee>, IRepositoryEmployee
    {
        public RepositoryEmployee(CodigoCertoContext context) : base(context)
        {
        }
    }
}
