using Course.Services.Discount.DTOs;
using Course.Services.Discount.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Course.Shared.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : CustomBaseController
    {
        readonly ISharedIdentityService _sharedIdentityService;
        readonly IDiscountService _discountService;

        public DiscountsController(ISharedIdentityService sharedIdentityService, IDiscountService discountService)
        {
            _sharedIdentityService = sharedIdentityService;
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDiscounts()
            => CreateActionResult(await _discountService.GetAllDiscountsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountById([FromRoute] int id)
            => CreateActionResult(await _discountService.GetDiscountByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> GetDiscountById([FromBody] CreateDiscountDto discount)
           => CreateActionResult(await _discountService.CreateDiscountAsync(discount));

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] UpdateDiscountDto discount)
           => CreateActionResult(await _discountService.UpdateDiscountAsync(discount));

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount([FromRoute] int id)
           => CreateActionResult(await _discountService.DeleteDiscountAsync(id));

        [HttpGet("[action]/{code}")]
        public async Task<IActionResult> GetDiscountByCode([FromRoute] string code)
            => CreateActionResult(await _discountService.GetDiscountByCodeAndUserIdAsync(code,_sharedIdentityService.UserId));
       

    }
}
