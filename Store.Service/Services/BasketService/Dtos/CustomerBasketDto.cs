using Store.Repository.Basket.Models;

namespace Store.Service.Services.BasketServcies.Dtos
{
    public class CustomerBasketDto
    {
        public string? Id { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public List<BasketItemsDto> BasketItems { get; set; } = new List<BasketItemsDto>();
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
