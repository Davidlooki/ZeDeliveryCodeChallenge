using Microsoft.EntityFrameworkCore;

namespace ZeDeliveryCodeChallenge
{
    public class PartnerRepository<T>(T appDbContext) : IPartnerRepository where T : DbContext, IPartnerDbSet
    {
        public async Task<Partner[]> GetPartners()
            => await appDbContext.Partners.ToArrayAsync();

        public async Task<Partner?> GetPartnerById(string id)
            => await appDbContext.Partners.FirstOrDefaultAsync(p => p.id == id);

        public async Task<Partner?> GetPartnerByDocument(string document)
            => await appDbContext.Partners.FirstOrDefaultAsync(p => p.document == document);

        public async Task<Partner> CreatePartner(Partner partner)
        {
            var result = await appDbContext.Partners.AddAsync(partner);
            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }
    }
}