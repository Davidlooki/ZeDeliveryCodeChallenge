using Microsoft.EntityFrameworkCore;

namespace ZeDeliveryCodeChallenge
{
    public class AppDbContext : DbContext, IPartnerDbSet
    {
        public DbSet<Partner> Partners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=admin;",
                pjr => pjr.UseNetTopologySuite());
        }
    }
}