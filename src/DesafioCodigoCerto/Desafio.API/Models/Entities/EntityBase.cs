namespace Desafio.API.Models.Entities
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