using Course.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Course.Shared.CustomBaseController
{
    public class CustomBaseController:ControllerBase
    {
        public IActionResult CreateActionResult<T>(ResponseDto<T> response)
            => new ObjectResult(response) { StatusCode = response.StatusCode };
    }
}
