using Microsoft.Extensions.Configuration;
using Store.Data.Entities;
using Store.Repository.Basket.Models;
using Store.Repository.Interfaces;
using Store.Service.Services.BasketServcies;
using Store.Service.Services.BasketServcies.Dtos;
using Store.Service.Services.OrderService.Dtos;
using Stripe;
using Product = Store.Data.Entities.Product;

namespace Store.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketService _basketService;

        public PaymentService(IConfiguration configuration,IUnitOfWork unitOfWork,IBasketService basketService)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _basketService = basketService;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntent(CustomerBasketDto basket)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

            if (basket == null)
                throw new Exception("Basket is empty");

            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod == null)
                throw new Exception("Delivery Method not Provided");

            decimal ShippingPrice = deliveryMethod.Price;

            foreach (var item in basket.BasketItems)
            {
                var product = await _unitOfWork.Repository<Product, int>().GetByIdAsync(item.ProductId);

                if(item.Price != product.Price)
                    item.Price = product.Price;
            }

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) +(long)(ShippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card"}
                };
                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)basket.BasketItems.Sum(item => item.Quantity * (item.Price * 100)) + (long)(ShippingPrice * 100),
                };
                await service.UpdateAsync(basket.PaymentIntentId,options);
            }
            await _basketService.UpdateBasketAsync(basket);

            return basket;
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetailsDto> UpdateOrderPaymentSucessed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
