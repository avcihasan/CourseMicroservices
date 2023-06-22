using Course.Services.PhotoStock.DTOs;
using Course.Services.PhotoStock.Services.Abstractions;
using Course.Shared.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Course.Services.PhotoStock.Services.Concretes
{
    public class PhotoService : IPhotoService
    {
        public ResponseDto<NoContentDto> DeletePhoto(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
                return ResponseDto<NoContentDto>.Fail("photo not found", HttpStatusCode.NotFound);
            System.IO.File.Delete(path);
            return ResponseDto<NoContentDto>.Success(HttpStatusCode.NoContent);

        }

        public async Task<ResponseDto<PhotoDto>> SavePhotoAsync(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo is null || photo.Length <= 0)
                return ResponseDto<PhotoDto>.Fail("photo is empty", HttpStatusCode.BadRequest);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = $"photos/{photo.FileName}";

            return ResponseDto<PhotoDto>.Success(new PhotoDto() { Url = returnPath }, HttpStatusCode.OK);

         
        }
    }
}
