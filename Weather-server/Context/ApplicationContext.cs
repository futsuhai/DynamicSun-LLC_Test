using Microsoft.EntityFrameworkCore;
using Weather_server.Models.Backend;

namespace Weather_server.Context
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        public DbSet<Weather> Weather { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return Set<T>();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }
    }
}