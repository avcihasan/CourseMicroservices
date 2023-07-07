using Course.Services.FakePayment.DTOs;
using Course.Services.FakePayment.Services.Abstractions;
using Course.Shared.Messages.Commands;
using MassTransit;

namespace Course.Services.FakePayment.Services.Concretes
{
    public class RabbitMQService : IRabbitMQService
    {
        readonly ISendEndpointProvider _sendEndpointProvider;

        public RabbitMQService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendMessageAsync(PaymentInfoDto paymentInfoDto)
        {
            ISendEndpoint sendEndpoint= await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));
            CreateOrderMessageCommand createOrderMessageCommand = new()
            {
                BuyerId = paymentInfoDto.Order.BuyerId,
                District = paymentInfoDto.Order.Address.District,
                Line = paymentInfoDto.Order.Address.Line,
                Province = paymentInfoDto.Order.Address.Province,
                Street = paymentInfoDto.Order.Address.Street,
                ZipCode = paymentInfoDto.Order.Address.ZipCode,
            };
            paymentInfoDto.Order.OrderItems.ForEach(x => createOrderMessageCommand.OrderItems.Add(new()
            {
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                CourseId = x.CourseId,
                CourseName = x.CourseName
            }));
            await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
        }
    }
}
