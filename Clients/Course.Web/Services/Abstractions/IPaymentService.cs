using Course.Web.Models.FakePaymentModels;

namespace Course.Web.Services.Abstractions
{
    public interface IPaymentService
    {
        Task<bool> RecievePayment(PaymentInfoVM payment);
    }
}
