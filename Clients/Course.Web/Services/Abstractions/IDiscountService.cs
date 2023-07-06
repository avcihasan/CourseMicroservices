using Course.Web.Models.DiscountModels;

namespace Course.Web.Services.Abstractions
{
    public interface IDiscountService
    {
        Task<DiscountVM> GetDiscountAsync(string discountCode);
    }
}
