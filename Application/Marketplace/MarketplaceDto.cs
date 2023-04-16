using Application.Profiles;

namespace Application.Marketplace
{
    public class MarketplaceDto
    {
        
        public Guid Id { get; set; }
        public string name { get; set; }
        public string account_server { get; set; }
        public string base_server { get; set; }
    }
}