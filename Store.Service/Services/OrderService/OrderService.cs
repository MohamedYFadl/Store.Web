using AutoMapper;
using Store.Data.Entities;
using Store.Data.Entities.OrderEntities;
using Store.Repository.Interfaces;
using Store.Service.Services.BasketServcies;
using Store.Service.Services.OrderService.Dtos;

namespace Store.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(
            IBasketService basketService,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderDetailsDto> CreateOrderAsync(OrderDto input)
        {
            var basket = await _basketService.GetBasketAsync(input.BasketId);
            if (basket == null)
                throw new Exception("Basket is not Exist");
            #region  Fill Order Item List WIth Items in the basket
            var orderItems = new List<OrderItemDto>();

            foreach (var BasketItem in basket.BasketItems)
            {
                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(BasketItem.ProductId);
                if (productItem == null)
                    throw new Exception($"Product With Id {BasketItem.ProductId} not exist");

                var ItemOrders = new ProductItem
                {
                    PictureUrl = productItem.PictureUrl,
                    ProductId = productItem.Id,
                    ProductName = productItem.Name,
                };

                var orderItem = new OrderItem
                {
                    Price = productItem.Price,
                    Quantity = BasketItem.Quantity,
                    ProductItem = ItemOrders,
                };

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItems);
                orderItems.Add(mappedOrderItem);

            }
            #endregion
            #region Get Delivery Method 
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetByIdAsync(input.DeliveryMethodId);
            if (deliveryMethod == null)
                throw new Exception("Delivery Method not Provided");


            #endregion
            #region Calc SubTotal
            var subtotal = orderItems.Sum(item=>item.Quantity*item.Price);
            #endregion
            #region To Do => Payment

            #endregion
            #region Create Order
            var MappedShippingAddress = _mapper.Map<ShippingAddress>(input.ShippingAddress);
            var MappedOrderItems = _mapper.Map<List<OrderItem>>(orderItems);
            var order = new Order
            {
                DeliveryMethodId = deliveryMethod.Id,
                ShippingAddress = MappedShippingAddress,
                BuyerEmail = input.BuyerEmail,
                BasketId = input.BasketId,
                Orderitems = MappedOrderItems,
                SubTotal = subtotal,
                
            };
            await _unitOfWork.Repository<Order,Guid>().AddAsync(order);
            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderDetailsDto>(order);
            return mappedOrder;
            #endregion
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
