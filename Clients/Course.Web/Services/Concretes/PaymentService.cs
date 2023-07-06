using Course.Web.Models.FakePaymentModels;
using Course.Web.Services.Abstractions;

namespace Course.Web.Services.Concretes
{
    public class PaymentService : IPaymentService
    {
        readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RecievePayment(PaymentInfoVM payment)
            =>(await _httpClient.PostAsJsonAsync<PaymentInfoVM>("fakepayments", payment)).IsSuccessStatusCode;
    }
}
