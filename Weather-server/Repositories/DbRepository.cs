using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Weather_server.Context;
using Weather_server.Models;
using Weather_server.Models.Backend;

namespace Weather_server.Repositories
{
    public class DbRepository(ApplicationContext context) : IDbRepository
    {
        private readonly ApplicationContext _context = context;

        public async Task<Guid> AddAsync<T>(T newEntity) where T : class, IEntity
        {
            var entity = await _context.Set<T>().AddAsync(newEntity);
            return entity.Entity.Id;
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> newEntities) where T : class, IEntity
        {
            await _context.Set<T>().AddRangeAsync(newEntities);
        }

        public async Task DeleteAsync<T>(Guid id) where T : class, IEntity
        {
            var activeEntity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
            if (activeEntity is not null)
            {
                await Task.Run(() => _context.Update<T>(activeEntity));
            }
        }

        public async Task<IQueryable<T>> GetAsync<T>(Expression<Func<T, bool>> selector) where T : class, IEntity
        {
            return await Task.Run(() => _context.Set<T>().Where(selector).AsQueryable());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T newEntity, Guid id) where T : class, IEntity
        {
            await Task.Run(() => _context.Set<T>().Update(newEntity));
        }
    }
}