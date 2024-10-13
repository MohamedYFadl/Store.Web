using Store.Data.Entities.OrderEntities;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithItemSpec : BaseSpecification<Order>
    {
        public OrderWithItemSpec(string buyerEmail) : base(order=>order.BuyerEmail == buyerEmail)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order=>order.Orderitems);
            AddOrderByDesc(order=>order.OrderDate);
        }
        public OrderWithItemSpec(Guid id) : base(order => order.Id == id)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.Orderitems);
        }
    }
}
