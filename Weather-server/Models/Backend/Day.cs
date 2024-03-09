namespace Weather_server.Models.Backend
{
    public class Day
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public List<Weather> Weathers { get; set; } = new List<Weather>();
    }
}