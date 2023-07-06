using Course.Shared.DTOs;
using Course.Web.Models.PhotoStockModels;
using Course.Web.Services.Abstractions;

namespace Course.Web.Services.Concretes
{
    public class PhotoStockService : IPhotoStockService
    {
        readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> DeletePhotoAsync(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos/{photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoVM> UploadPhotoAsync(IFormFile photo)
        {
            if (photo is null || photo.Length < 0)
                return null;

            string randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            MultipartFormDataContent multipartContent = new()
            {
                { new ByteArrayContent(ms.ToArray()), "photo", randomFileName }
            };

            var response = await _httpClient.PostAsync("photos", multipartContent);

            if (!response.IsSuccessStatusCode)
                return null;
            return (await response.Content.ReadFromJsonAsync<ResponseDto<PhotoVM>>()).Data;
        }
    }
}
