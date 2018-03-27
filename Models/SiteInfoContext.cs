using Microsoft.EntityFrameworkCore;

namespace SMATMVC.Models
{
    public class SiteInfoContext : DbContext
    {
        public SiteInfoContext(DbContextOptions<SiteInfoContext> options) : base(options)
        {

        }

        public DbSet<SiteInfo> SiteInfo { get; set; }
    }
}