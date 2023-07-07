using Course.Shared.DTOs;
using Course.Shared.Services.Abstractions;
using Course.Web.Models.BasketModels;
using Course.Web.Models.FakePaymentModels;
using Course.Web.Models.OrderModels;
using Course.Web.Services.Abstractions;

namespace Course.Web.Services.Concretes
{
    public class OrderService : IOrderService
    {
        readonly HttpClient _httpClient;
        readonly IPaymentService _paymentService;
        readonly IBasketService _basketService;
        readonly ISharedIdentityService _sharedIdentityService;
        public OrderService(HttpClient httpClient, IPaymentService paymentService, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _httpClient = httpClient;
            _paymentService = paymentService;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedVM> CreateOrderAsync(CheckoutInfoVM checkoutInfoVM)
        {
            BasketVM basket = await _basketService.GetBasketAsync();

            PaymentInfoVM paymentInfoVM = new()
            {
                CardName = checkoutInfoVM.CardName,
                CardNumber = checkoutInfoVM.CardNumber,
                CVV = checkoutInfoVM.CVV,
                Expiration = checkoutInfoVM.Expiration,
                TotalPrice = basket.TotalPrice
            };

            bool paymentResponse = await _paymentService.ReceivePayment(paymentInfoVM);
            if (!paymentResponse)
                return new() { Error = "Ödeme Hatası", IsSuccessful = false };

            CreateOrderVM createOrderVM = new()
            {
                Address = checkoutInfoVM.Address,
                BuyerId = _sharedIdentityService.UserId
            };
            basket.BasketItems.ForEach(x => createOrderVM.OrderItems.Add(new()
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                PictureUrl = "",
                Price = x.CurrentPrice
            }));

            HttpResponseMessage httpResponseMessage = await _httpClient.PostAsJsonAsync<CreateOrderVM>("orders", createOrderVM);
            if (!httpResponseMessage.IsSuccessStatusCode)
                return new() { Error = "Sipariş Oluşturma Hatası", IsSuccessful = false };
            await _basketService.DeleteBasketAsync();
            return (await httpResponseMessage.Content.ReadFromJsonAsync<ResponseDto<OrderCreatedVM>>()).Data;
        }

        public async Task<List<OrderVM>> GetAllOrderAsync()
           => (await _httpClient.GetFromJsonAsync<ResponseDto<List<OrderVM>>>("orders")).Data;

        public async Task<OrderSuspendVM> SuspendOrderAsync(CheckoutInfoVM checkoutInfoVM)
        {

            BasketVM basket = await _basketService.GetBasketAsync();
            CreateOrderVM createOrderVM = new()
            {
                BuyerId = _sharedIdentityService.UserId,
                Address = checkoutInfoVM.Address,
            };

            basket.BasketItems.ForEach(x => createOrderVM.OrderItems.Add(new()
            {
                CourseId = x.CourseId,
                CourseName = x.CourseName,
                PictureUrl = "",
                Price = x.CurrentPrice
            }));

            PaymentInfoVM paymentInfoVM = new()
            {
                CardName = checkoutInfoVM.CardName,
                CardNumber = checkoutInfoVM.CardNumber,
                Expiration = checkoutInfoVM.Expiration,
                CVV = checkoutInfoVM.CVV,
                TotalPrice = basket.TotalPrice,
                Order = createOrderVM
            };

            bool paymentResponse = await _paymentService.ReceivePayment(paymentInfoVM);
            if (!paymentResponse)
                return new() { Error = "Ödeme Hatası", IsSuccessful = false };

            await _basketService.DeleteBasketAsync();
            return new() { IsSuccessful = true };
        }
    }
}
