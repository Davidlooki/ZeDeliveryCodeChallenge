namespace ZeDeliveryCodeChallenge
{
    public interface IPartnerDbSet
    {
        public Microsoft.EntityFrameworkCore.DbSet<Partner> Partners { get; set; }
    }
}