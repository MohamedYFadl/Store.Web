namespace Store.Repository.Basket.Models
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItems> BasketItems { get; set; } = new List<BasketItems>();
    }
}
