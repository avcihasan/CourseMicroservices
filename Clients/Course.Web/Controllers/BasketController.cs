using Course.Web.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Web.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        readonly ICatalogService _catalogService;
        readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
            => View(await _basketService.GetBasketAsync());
        public async Task<IActionResult> AddBasketItemToBasket(string courseId)
        {
            await _basketService.AddBasketItemToBasketAsync(courseId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveBasketItemToBasket(string courseId)
        {
            await _basketService.RemoveBasketItemToBasketAsync(courseId);
            return RedirectToAction(nameof(Index));
        }
    }
}
