using DynamicSunTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTestTask.Contexts
{
    public class RecordsContext : DbContext
    {
        public RecordsContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Record> Records { get; set; }
    }
}
