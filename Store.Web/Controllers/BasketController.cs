using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketServcies;
using Store.Service.Services.BasketServcies.Dtos;

namespace Store.Web.Controllers
{

    public class BasketController : BaseController
    {
        private readonly IBasketService _basketServices;

        public BasketController(IBasketService basketServices)
        {
            _basketServices = basketServices;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketAsync(string Id)
            => Ok(await _basketServices.GetBasketAsync(Id));
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto input)
            => Ok(await _basketServices.UpdateBasketAsync(input));
        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string Id)
             => Ok(await _basketServices.DeleteBasketAsync(Id));
    }
}
