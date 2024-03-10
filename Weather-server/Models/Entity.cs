namespace Weather_server.Models
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }
    }
}