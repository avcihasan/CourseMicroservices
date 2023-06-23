using Course.Services.Basket.DTOs;
using Course.Services.Basket.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
            => CreateActionResult(await _basketService.GetBasketAsync());
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basket)
           => CreateActionResult(await _basketService.SaveOrUpdateBasketAsync(basket));
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
           => CreateActionResult(await _basketService.DeleteBasketAsync());
    }
}
