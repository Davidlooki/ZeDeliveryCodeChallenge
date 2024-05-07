using Microsoft.EntityFrameworkCore;

namespace ZeDeliveryCodeChallenge
{
    public class AppDbContext : DbContext, IPartnerDbSet
    {
        public DbSet<Partner> Partners { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}