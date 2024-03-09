namespace Weather_server.Models.Backend
{
    public class Weather
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double Td { get; set; }
        public int Pressure { get; set; }
        public string WindDirection { get; set; } = string.Empty;
        public int WindSpeed { get; set; }
        public int Cloudy { get; set; }
        public int H { get; set; }
        public int VV { get; set; }
        public string WeatherCondition { get; set; } = string.Empty;
    }
}