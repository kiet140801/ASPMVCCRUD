using Microsoft.EntityFrameworkCore;
using WEDASPNETCORECRUD.Models.Domail;

namespace WEDASPNETCORECRUD.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {
        }
    public DbSet<Teacher> Teachers { get; set; }

    }
}
