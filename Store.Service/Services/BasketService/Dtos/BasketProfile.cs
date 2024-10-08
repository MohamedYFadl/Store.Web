using AutoMapper;
using Store.Repository.Basket.Models;

namespace Store.Service.Services.BasketServcies.Dtos
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemsDto>().ReverseMap();
        }
    }
}
