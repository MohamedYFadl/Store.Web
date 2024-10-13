using Store.Service.Services.BasketServcies.Dtos;
using Store.Service.Services.OrderService.Dtos;

namespace Store.Service.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto input);
        Task<OrderDetailsDto> UpdateOrderPaymentSucessed(string paymentIntentId);
        Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
