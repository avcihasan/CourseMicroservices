using Course.Services.PhotoStock.Services.Abstractions;
using Course.Shared.CustomBaseController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        readonly IPhotoService _photoService;

        public PhotosController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> SavePhoto(IFormFile photo, CancellationToken cancellationToken)
            => CreateActionResult(await _photoService.SavePhotoAsync(photo,cancellationToken));
        [HttpDelete]
        public IActionResult DeletePhoto(string photoUrl)
            => CreateActionResult(_photoService.DeletePhoto(photoUrl));
    }
}
