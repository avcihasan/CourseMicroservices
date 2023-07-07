using Course.Services.Basket.DTOs;
using Course.Services.Basket.Services.Abstractions;
using Course.Services.Basket.Services.Concretes;
using Course.Shared.DTOs;
using Course.Shared.Messages.Events;
using Course.Shared.Services.Abstractions;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace Course.Services.Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<CourseNameChangedEvent>
    {
        readonly IRedisService _redisService;
        readonly ISharedIdentityService _sharedIdentityService;

        public CourseNameChangedEventConsumer(IRedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task Consume(ConsumeContext<CourseNameChangedEvent> context)
        {
            
            BasketDto basket = JsonSerializer.Deserialize<BasketDto>(await _redisService.GetDb().StringGetAsync(_sharedIdentityService.UserId));

            basket.BasketItems.ForEach(x =>
            {
                if (x.CourseId == context.Message.CourseId)
                    x.CourseName = context.Message.UpdatedName;
            });
            await _redisService.GetDb().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
        }
    }
}
