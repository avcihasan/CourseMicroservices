using Course.Services.FakePayment.DTOs;
using Course.Services.FakePayment.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Course.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Course.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        readonly IRabbitMQService _rabbitMQService;

        public FakePaymentsController(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost]
        public async Task<IActionResult> Payment([FromBody]PaymentInfoDto paymentInfoDto)
        {
            await _rabbitMQService.SendMessageAsync(paymentInfoDto);
            return CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
        }
    }
}
