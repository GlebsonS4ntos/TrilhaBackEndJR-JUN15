namespace Desafio.API.Models
{
    public class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public EntityBase() 
        { 
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}