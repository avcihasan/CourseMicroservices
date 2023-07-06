using Course.Shared.DTOs;
using Course.Web.Models.DiscountModels;
using Course.Web.Services.Abstractions;

namespace Course.Web.Services.Concretes
{
    public class DiscountService : IDiscountService
    {
        readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountVM> GetDiscountAsync(string discountCode)
        {
          HttpResponseMessage responseMessage=  await _httpClient.GetAsync($"discounts/GetDiscountByCode/{discountCode}");
            if (!responseMessage.IsSuccessStatusCode)
                return null;
            return (await responseMessage.Content.ReadFromJsonAsync<ResponseDto<DiscountVM>>()).Data;
        }
    }
}
