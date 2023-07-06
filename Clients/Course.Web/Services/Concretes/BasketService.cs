using Course.Shared.DTOs;
using Course.Web.Models.BasketModels;
using Course.Web.Models.CatalogModels;
using Course.Web.Services.Abstractions;

namespace Course.Web.Services.Concretes
{
    public class BasketService : IBasketService
    {
        readonly HttpClient _httpClient;
        readonly ICatalogService _catalogService;

        public BasketService(HttpClient httpClient, ICatalogService catalogService)
        {
            _httpClient = httpClient;
            _catalogService = catalogService;
        }

        public async Task AddBasketItemToBasketAsync(string  courseId)
        {
            CourseVM course =await _catalogService.GetCourseByIdAsync(courseId);
            BasketItemVM basketItemVm = new() { CourseId = courseId, CourseName = course.Name, CoursePrice = course.Price };
            BasketVM basket = await GetBasketAsync();
            if (basket != null)
            {
                if (!basket.BasketItems.Any(x => x.CourseId == basketItemVm.CourseId))
                    basket.BasketItems.Add(basketItemVm);
            }

            else
            {
                basket = new();
                basket.BasketItems.Add(basketItemVm);
            }
            await SaveOrUpdateAsync(basket);
                
        }

        public async Task<bool> ApplyDiscountAsync(string discountCode)
        {
            await CancelDiscountAsync();
            BasketVM basket = await GetBasketAsync();
            basket.DiscountCode = discountCode;
            return true;
        }

        public async Task<bool> CancelDiscountAsync()
        {
            BasketVM basket = await GetBasketAsync();
            if (basket is null)
                basket.DiscountCode = string.Empty;
            return true;
        }

        public async Task<bool> DeleteBasketAsync()
        {
            HttpResponseMessage responseMessage = await _httpClient.DeleteAsync("baskets");
            return (await responseMessage.Content.ReadFromJsonAsync<ResponseDto<bool>>()).Data;

        }

        public async Task<BasketVM> GetBasketAsync()
        {
           HttpResponseMessage responseMessage= await _httpClient.GetAsync("baskets");
            if (!responseMessage.IsSuccessStatusCode)
                return null;
           return  (await responseMessage.Content.ReadFromJsonAsync<ResponseDto<BasketVM>>()).Data;
        }

        public async Task<bool> RemoveBasketItemToBasketAsync(string courseId)
        {
            BasketVM basket = await GetBasketAsync();
            if (basket is null)
                return false;
            BasketItemVM basketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (basketItem is null)
                return false;

            bool result = basket.BasketItems.Remove(basketItem);

            if (result)
            {
                if (!basket.BasketItems.Any())
                    basket.DiscountCode = null;
                await SaveOrUpdateAsync(basket);
            }
            return result;
        }

        public async Task<bool> SaveOrUpdateAsync(BasketVM basketVm)
            => (await _httpClient.PostAsJsonAsync<BasketVM>("baskets", basketVm)).IsSuccessStatusCode;
    }
}
