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
        [HttpPost]
        public IActionResult Payment()
            => CreateActionResult(ResponseDto<NoContentDto>.Success(HttpStatusCode.OK));
    }
}
