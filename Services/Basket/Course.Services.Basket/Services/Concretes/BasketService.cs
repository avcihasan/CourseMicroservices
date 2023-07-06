using Course.Services.Basket.DTOs;
using Course.Services.Basket.Services.Abstractions;
using Course.Shared.DTOs;
using Course.Shared.Services.Abstractions;
using System.Net;
using System.Text.Json;

namespace Course.Services.Basket.Services.Concretes
{
    public class BasketService : IBasketService
    {
        readonly IRedisService _redisService;
        readonly ISharedIdentityService _sharedIdentityService;

        public BasketService(IRedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<ResponseDto<bool>> DeleteBasketAsync()
        {
            bool result = await _redisService.GetDb().KeyDeleteAsync(_sharedIdentityService.UserId);

            return result ? ResponseDto<bool>.Success(HttpStatusCode.NoContent) : ResponseDto<bool>.Fail("Basket not found", HttpStatusCode.NotFound);
        }

        public async Task<ResponseDto<BasketDto>> GetBasketAsync()
        {
            var basket = await _redisService.GetDb().StringGetAsync(_sharedIdentityService.UserId);
            if (String.IsNullOrEmpty(basket))
                return ResponseDto<BasketDto>.Fail("Basket not found", HttpStatusCode.NotFound);
            return ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(basket), HttpStatusCode.OK);
        }

        public async Task<ResponseDto<bool>> SaveOrUpdateBasketAsync(BasketDto basket)
        {
            basket.UserId = _sharedIdentityService.UserId;

            bool result = await _redisService.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));

            return result ? ResponseDto<bool>.Success(HttpStatusCode.NoContent) : ResponseDto<bool>.Fail("Basket could not save or update", HttpStatusCode.InternalServerError);
        }
    }
}
