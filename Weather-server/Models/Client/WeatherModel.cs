namespace Weather_server.Models.Client
{
    public class WeatherModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Td { get; set; }
        public int Pressure { get; set; }
        public string WindDirection { get; set; } = string.Empty;
        public int WindSpeed { get; set; }
        public float Cloudy { get; set; }
        public int H { get; set; }
        public int VV { get; set; }
        public string WeatherCondition { get; set; } = string.Empty;
    }
}