using Course.Services.PhotoStock.DTOs;
using Course.Shared.DTOs;

namespace Course.Services.PhotoStock.Services.Abstractions
{
    public interface IPhotoService
    {
        Task<ResponseDto<PhotoDto>> SavePhotoAsync(IFormFile photo, CancellationToken cancellationToken);
        ResponseDto<NoContentDto> DeletePhoto(string photoUrl);
    }
}
