using Microsoft.EntityFrameworkCore;
using Weather_server.Models.Backend;

namespace Weather_server.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Day> Days { get; set; }
    }
}