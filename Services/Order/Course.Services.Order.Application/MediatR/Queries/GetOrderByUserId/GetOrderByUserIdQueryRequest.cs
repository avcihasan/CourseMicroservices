using Course.Services.Order.Application.DTOs;
using Course.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.MediatR.Queries.GetOrderByUserId
{
    public class GetOrderByUserIdQueryRequest:IRequest<ResponseDto<List<OrderDto>>>
    {
        public string BuyerId { get; set; }
    }
}
