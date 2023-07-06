using Course.Web.Models.BasketModels;

namespace Course.Web.Services.Abstractions
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdateAsync(BasketVM basketVm);
        Task<BasketVM> GetBasketAsync();
        Task<bool> DeleteBasketAsync();
        Task AddBasketItemToBasketAsync(string courseId);
        Task<bool> RemoveBasketItemToBasketAsync(string  courseId);
        Task<bool> ApplyDiscountAsync(string  discountCode);
        Task<bool> CancelDiscountAsync();
    }
}
