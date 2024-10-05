using Store.Repository.Basket.Models;
using Store.Service.Services.BasketServcies.Dtos;

namespace Store.Service.Services.BasketServcies
{
    public interface IBasketServices
    {
        Task<CustomerBasketDto> GetBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
