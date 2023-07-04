using Course.Services.Order.Application.DTOs;
using Course.Services.Order.Domain.OrderAggregate;
using Course.Services.Order.Infrastructure.Contexts;
using Course.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.MediatR.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, ResponseDto<CreatedOrderDto>>
    {
        readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<ResponseDto<CreatedOrderDto>> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            Address address = new(
                province: request.Address.Province,
                district: request.Address.District,
                street: request.Address.Street,
                zipCode: request.Address.ZipCode,
                line: request.Address.Line);

            Domain.OrderAggregate.Order order = new(request.BuyerId, address);
            request.OrderItems.ForEach(x => order.AddOrderItem(courseId: x.CourseId, courseName: x.CourseName, price: x.Price, pictureUrl: x.PictureUrl));

            await _orderDbContext.Orders.AddAsync(order);
            await _orderDbContext.SaveChangesAsync();

            return ResponseDto<CreatedOrderDto>.Success(new CreatedOrderDto() { Id = order.Id }, HttpStatusCode.OK);

        }
    }
}
