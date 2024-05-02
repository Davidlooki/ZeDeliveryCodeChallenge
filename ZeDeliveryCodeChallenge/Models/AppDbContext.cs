using Microsoft.EntityFrameworkCore;

namespace ZeDeliveryCodeChallenge
{
    public class AppDbContext : DbContext, IPartnerDbSet
    {
        public AppDbContext() { } //Migration requires an empty constructor to work.
        public DbSet<Partner> Partners { get; set; }
    }
}