namespace Domain
{
    public class Shop
    {
        public Guid Id { get; set; }
        public string Client_id { get; set; }
        public string Client_secret { get; set; }
        public Guid Marketplace_id { get; set; }

        public Marketplace Marketplace { get; set; }
    }
}