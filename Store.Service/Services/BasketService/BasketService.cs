using AutoMapper;
using Store.Repository.Basket;
using Store.Repository.Basket.Models;
using Store.Service.Services.BasketServcies.Dtos;

namespace Store.Service.Services.BasketServcies
{
    public class BasketService : IBasketService
    {
        private readonly BasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(BasketRepository basketRepository,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
         => await _basketRepository.DeleteBasketAsync(basketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if(basket == null)
                return new CustomerBasketDto();

            var mappedBasket = _mapper.Map<CustomerBasketDto>(basket);
            return mappedBasket;
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto input)
        {
            if (input.Id == null)
                input.Id = GenerateRandomBasketId();
            var customerBasket = _mapper.Map<CustomerBasket>(input);
            var updatedBasket = await _basketRepository.UpdateBasketAsync(customerBasket);
            var basket = _mapper.Map<CustomerBasketDto>(updatedBasket);

            return basket;
        }
        private string GenerateRandomBasketId() {
            var random = new Random();
            int randomDigit = random.Next(1000,10000);
            return $"BS-{randomDigit}";
        }
    }
}
