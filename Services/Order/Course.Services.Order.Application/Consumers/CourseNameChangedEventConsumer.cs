using Course.Services.Order.Domain.OrderAggregate;
using Course.Services.Order.Infrastructure.Contexts;
using Course.Shared.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Services.Order.Application.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        readonly OrderDbContext _orderDbContext;

        public CourseNameChangedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            List<OrderItem> orderList =await _orderDbContext.OrderItems.Where(x => x.CourseId == context.Message.CourseId).ToListAsync();

            orderList.ForEach(x => x.UpdateOrderItem(context.Message.UpdatedName,x.PictureUrl,x.Price));

            await _orderDbContext.SaveChangesAsync();
        }
    }
}
