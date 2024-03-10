using Weather_server.Models.Backend;

namespace Weather_server.Services.DayService
{
    public interface IWeatherService
    {
         public Task<Weather?> GetAsync(Guid id);

        public Task<IList<Weather>> GetAllAsync(DateTime date);

        public Task<Guid> AddAsync(Weather entity);

        public Task AddRangeAsync(IEnumerable<Weather> newEntities);

        public Task DeleteAsync(Guid id);

        public Task UpdateAsync(Weather newEntity, Guid id);
    }
}