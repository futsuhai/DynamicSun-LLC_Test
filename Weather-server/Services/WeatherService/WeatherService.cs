using Weather_server.Models.Backend;
using Weather_server.Repositories;

namespace Weather_server.Services.DayService
{
    public class WeatherService(IDbRepository dbRepository) : IWeatherService
    {
        private readonly IDbRepository _dbRepository = dbRepository;

        public async Task<Guid> AddAsync(Weather weather)
        {
            var id = await _dbRepository.AddAsync(weather);
            await _dbRepository.SaveChangesAsync();
            return id;
        }

        public async Task AddRangeAsync(IEnumerable<Weather> weathers)
        {
            await _dbRepository.AddRangeAsync(weathers);
            await _dbRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _dbRepository.DeleteAsync<Weather>(id);
            await _dbRepository.SaveChangesAsync();
        }

        public async Task<IList<Weather>> GetAllAsync(DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1);
            var weathers = await _dbRepository.GetAsync<Weather>(weather => weather.Date >= startDate && weather.Date < endDate);
            var sortedWeathers = weathers.OrderBy(weather => weather.Date).ToList();
            return [.. sortedWeathers];
        }

        public async Task<Weather?> GetAsync(Guid id)
        {
            var weather = await _dbRepository.GetAsync<Weather>(weather => weather.Id == id);
            return (Weather?)weather;
        }

        public async Task UpdateAsync(Weather weather, Guid id)
        {
            await _dbRepository.UpdateAsync<Weather>(weather, id);
            await _dbRepository.SaveChangesAsync();
        }
    }
}