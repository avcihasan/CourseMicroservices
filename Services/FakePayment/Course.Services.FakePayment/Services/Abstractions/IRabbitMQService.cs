using Course.Services.FakePayment.DTOs;

namespace Course.Services.FakePayment.Services.Abstractions
{
    public interface IRabbitMQService
    {
        Task SendMessageAsync(PaymentInfoDto paymentInfoDto);
    }
}
