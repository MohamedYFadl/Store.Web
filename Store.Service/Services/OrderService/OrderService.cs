using Store.Data.Entities;
using Store.Service.Services.OrderService.Dtos;

namespace Store.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        public Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrderDetailsDto>> GetAllOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailsDto> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
