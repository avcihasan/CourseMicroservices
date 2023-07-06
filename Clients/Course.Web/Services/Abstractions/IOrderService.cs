using Course.Web.Models.OrderModels;

namespace Course.Web.Services.Abstractions
{
    public interface IOrderService
    {
        /// <summary>
        /// Senkron İletişim
        /// </summary>
        /// <param name="checkoutInfoVM"></param>
        /// <returns></returns>
        Task<OrderCreatedVM> CreateOrderAsync(CheckoutInfoVM checkoutInfoVM);
        /// <summary>
        /// Asenkron İletişim
        /// </summary>
        /// <param name="checkoutInfoVM"></param>
        /// <returns></returns>
        Task SuspendOrder(CheckoutInfoVM checkoutInfoVM);

        Task<List<OrderVM>> GetAllOrderAsync(); 

    }
}
