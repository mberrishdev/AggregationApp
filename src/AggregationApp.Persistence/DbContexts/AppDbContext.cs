using AggregationApp.Domain.RegionMetering;
using Microsoft.EntityFrameworkCore;

namespace AggregationApp.Persistence.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionDetail> RegionDetails { get; set; }
    }
}
