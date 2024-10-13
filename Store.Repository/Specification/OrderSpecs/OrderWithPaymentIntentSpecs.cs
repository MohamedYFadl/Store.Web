using Store.Data.Entities.OrderEntities;
using System.Linq.Expressions;

namespace Store.Repository.Specification.OrderSpecs
{
    public class OrderWithPaymentIntentSpecs : BaseSpecification<Order>
    {
        public OrderWithPaymentIntentSpecs(string? paymentIntentId) : base(order=>order.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
