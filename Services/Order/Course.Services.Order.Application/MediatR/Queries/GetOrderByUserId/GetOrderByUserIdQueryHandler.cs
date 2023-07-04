using AutoMapper;
using Course.Services.Order.Application.DTOs;
using Course.Services.Order.Infrastructure.Contexts;
using Course.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Course.Services.Order.Application.MediatR.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrderByUserIdQueryRequest, ResponseDto<List<OrderDto>>>
    {
        readonly IMapper _mapper;
        readonly OrderDbContext _context;

        public GetOrderByUserIdQueryHandler(IMapper mapper, OrderDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrderByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            List<Domain.OrderAggregate.Order> orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.BuyerId).ToListAsync();
            if (!orders.Any())
                return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(),HttpStatusCode.OK);

            return ResponseDto<List<OrderDto>>.Success(_mapper.Map<List<OrderDto>>(orders), HttpStatusCode.OK);

        }
    }
}
