namespace Desafio.API.Models.Dtos
{
    public class EmployeeDto
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DateTime DataNasc { get; set; }
        public string Cpf { get; set; }
    }
}
