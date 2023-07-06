using Course.Web.Models.DiscountModels;
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

        public async Task<IActionResult> ApplyDiscount(DiscountApplyVM discountApplyVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction(nameof(Index));
            }
            bool result =await _basketService.ApplyDiscountAsync(discountApplyVM.Code);
            TempData["discountStatus"] = result;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelDiscount(DiscountApplyVM discount)
        {
            await _basketService.CancelDiscountAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
