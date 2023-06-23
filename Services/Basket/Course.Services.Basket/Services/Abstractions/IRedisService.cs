using StackExchange.Redis;
using System.Threading.Tasks;

namespace Course.Services.Basket.Services.Abstractions
{
    public interface IRedisService
    {
        Task ConnectAsync();
        IDatabase GetDb(int db = 1);
    }
}
