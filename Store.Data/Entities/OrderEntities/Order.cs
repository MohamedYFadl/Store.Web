namespace Store.Data.Entities.OrderEntities
{
    public class Order:BaseEntity<Guid>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Placed;
        public OrderPaymentStatus OrderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public IReadOnlyList<OrderItem> Orderitems { get; set; }
        public string? BasketId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal() 
            =>SubTotal+DeliveryMethod.Price;

        public string? PaymentIntentId { get; set; }

    }
}
