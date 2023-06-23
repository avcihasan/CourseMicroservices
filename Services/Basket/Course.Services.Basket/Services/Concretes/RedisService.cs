using Course.Services.Basket.Services.Abstractions;
using StackExchange.Redis;

namespace Course.Services.Basket.Services.Concretes
{
    public class RedisService: IRedisService
    {
        readonly string _host;
        readonly int _port;
        ConnectionMultiplexer _connectionMultiplexer;
        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }

        public async Task ConnectAsync() => _connectionMultiplexer =await ConnectionMultiplexer.ConnectAsync($"{_host}:{_port}");
        public IDatabase GetDb(int db=1) => _connectionMultiplexer.GetDatabase(db);
    }
}
