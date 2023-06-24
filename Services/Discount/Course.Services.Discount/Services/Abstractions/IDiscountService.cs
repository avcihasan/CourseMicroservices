using Course.Services.Discount.DTOs;
using Course.Shared.DTOs;

namespace Course.Services.Discount.Services.Abstractions
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<DiscountDto>>> GetAllDiscountsAsync();
        Task<ResponseDto<DiscountDto>> GetDiscountByIdAsync(int id);
        Task<ResponseDto<NoContentDto>> CreateDiscountAsync(CreateDiscountDto discount);
        Task<ResponseDto<NoContentDto>> UpdateDiscountAsync(UpdateDiscountDto discount);
        Task<ResponseDto<NoContentDto>> DeleteDiscountAsync(int id);
        Task<ResponseDto<DiscountDto>> GetDiscountByCodeAndUserIdAsync(string code,string userId);

    }
}
