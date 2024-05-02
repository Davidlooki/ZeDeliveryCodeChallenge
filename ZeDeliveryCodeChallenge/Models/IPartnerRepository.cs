namespace ZeDeliveryCodeChallenge
{
    public interface IPartnerRepository
    {
        Task<Partner[]> GetPartners();
        Task<Partner?> GetPartnerById(string id);
        Task<Partner?> GetPartnerByDocument(string document);
        Task<Partner> CreatePartner(Partner partner);
    }
}