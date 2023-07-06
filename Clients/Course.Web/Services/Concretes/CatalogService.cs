using Course.Shared.DTOs;
using Course.Web.Helpers;
using Course.Web.Models;
using Course.Web.Models.CatalogModels;
using Course.Web.Models.PhotoStockModels;
using Course.Web.Services.Abstractions;
using System.Net.Http.Json;

namespace Course.Web.Services.Concretes
{
    public class CatalogService : ICatalogService
    {
        readonly HttpClient _httpClient;
        readonly IPhotoStockService _photoStockService;
        readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCourseAsync(CreateCourseVM createCourseVM)
        {
          PhotoVM photoVM =  await _photoStockService.UploadPhotoAsync(createCourseVM.PhotoFormFile);
            if (photoVM is not null)
                createCourseVM.Picture = photoVM.Url;
            return (await _httpClient.PostAsJsonAsync<CreateCourseVM>("courses", createCourseVM)).IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
             => (await _httpClient.DeleteAsync($"courses/{courseId}")).IsSuccessStatusCode;

        public async Task<List<CategoryVM>> GetAllCategoriesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
                return null;
            return (await response.Content.ReadFromJsonAsync<ResponseDto<List<CategoryVM>>>()).Data;
        }

        public async Task<List<CourseVM>> GetAllCoursesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
                return null;
            List<CourseVM> data = (await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseVM>>>()).Data;
            data.ForEach(x => x.StockPictureUrl = _photoHelper.GetPhotoUrl(x.Picture));

            return data;
        }

        public async Task<List<CourseVM>> GetAllCoursesByUserIdAsync(string userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"courses/GetAllCourseByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
                return null;

            List<CourseVM> data = (await response.Content.ReadFromJsonAsync<ResponseDto<List<CourseVM>>>()).Data;
            data.ForEach(x =>x.StockPictureUrl = _photoHelper.GetPhotoUrl(x.Picture));

            return data;
        }

        public async Task<CourseVM> GetCourseByIdAsync(string courseId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
                return null;

            CourseVM course = (await response.Content.ReadFromJsonAsync<ResponseDto<CourseVM>>()).Data;
            course.StockPictureUrl = _photoHelper.GetPhotoUrl(course.Picture);

            return course;
        }

        public async Task<bool> UpdatdeCourseAsync(UpdateCourseVM updateCourseVM)
        {
            PhotoVM photoVM = await _photoStockService.UploadPhotoAsync(updateCourseVM.PhotoFormFile);
            if (photoVM is not null)
            {
               await _photoStockService.DeletePhotoAsync(photoVM.Url);
                updateCourseVM.Picture = photoVM.Url;
            }
            return  (await _httpClient.PutAsJsonAsync<UpdateCourseVM>("courses", updateCourseVM)).IsSuccessStatusCode;

        }
    }
}
