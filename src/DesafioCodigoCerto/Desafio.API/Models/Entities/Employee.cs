namespace Desafio.API.Models.Entities
{
    public class Employee : EntityBase
    {
        public string Name { get; set; }
        public DateTime DataNasc { get; set; }
        public string Cpf { get; set; }
    }
}