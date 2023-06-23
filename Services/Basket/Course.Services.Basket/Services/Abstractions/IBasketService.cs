using Course.Services.Basket.DTOs;
using Course.Shared.DTOs;

namespace Course.Services.Basket.Services.Abstractions
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasketAsync();
        Task<ResponseDto<bool>> SaveOrUpdateBasketAsync(BasketDto basket);
        Task<ResponseDto<bool>> DeleteBasketAsync();
    }
}
