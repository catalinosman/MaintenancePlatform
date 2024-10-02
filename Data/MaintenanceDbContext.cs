using MaintenancePlatform.Shared;
using Microsoft.EntityFrameworkCore;

namespace MaintenancePlatform.Data
{
    public class MaintenanceDbContext : DbContext
    {
        public DbSet<MaintenanceSheet> MaintenanceSheets { get; set; }

        public MaintenanceDbContext(DbContextOptions<MaintenanceDbContext> options) : base(options)
        {
              
        }
    }
}
