using Course.Web.Models.PhotoStockModels;

namespace Course.Web.Services.Abstractions
{
    public interface IPhotoStockService
    {
        Task<PhotoVM> UploadPhotoAsync(IFormFile photo);
        Task<bool> DeletePhotoAsync(string photoUrl);
    }
}
