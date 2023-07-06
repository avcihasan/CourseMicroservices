using Course.Web.Models.OrderModels;
using Course.Web.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;

        public OrdersController(IOrderService orderService, IBasketService basketService)
        {
            _orderService = orderService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Checkout()
        {
            ViewBag.basket = await _basketService.GetBasketAsync();
            return View(new CheckoutInfoVM());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoVM checkoutInfoVM)
        {
            OrderCreatedVM response=  await _orderService.CreateOrderAsync(checkoutInfoVM);
            if (!response.IsSuccessful)
            {
                ViewBag.basket = await _basketService.GetBasketAsync();
                ViewBag.error = response.Error;
                return View();
            }
            return RedirectToAction(nameof(SuccessfulCheckout), new { id = response.Id});
        }

        public IActionResult SuccessfulCheckout(int id)
        {
            ViewBag.orderId = id;

            return View();
        }
    }
}
