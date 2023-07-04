using Course.Services.Order.Application.MediatR.Commands.CreateOrder;
using Course.Services.Order.Application.MediatR.Queries.GetOrderByUserId;
using Course.Shared.CustomBaseController;
using Course.Shared.Services.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        readonly ISharedIdentityService _sharedIdentityService;
        readonly IMediator _mediator;

        public OrdersController(ISharedIdentityService sharedIdentityService, IMediator mediator)
        {
            _sharedIdentityService = sharedIdentityService;
            _mediator = mediator;
        }

        [HttpGet] 
        public  async Task<IActionResult> GetOrders()
            => CreateActionResult(await _mediator.Send(new GetOrderByUserIdQueryRequest() { BuyerId= _sharedIdentityService.UserId }));
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
            => CreateActionResult(await _mediator.Send(request));
    }
}
