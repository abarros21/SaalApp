using StackExchange.Redis;
using WarrehouseApp.Infrastructure.Data.Interfaces.Redis;

namespace WarrehouseApp.Infrastructure.Data.Repositories.Redis
{
    public class RedisRepository : IRedisRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly IServer _server;

        public RedisRepository(string connectionString)
        {
            _redis = ConnectionMultiplexer.Connect(connectionString);
            _database = _redis.GetDatabase();
            _server = _redis.GetServer(_redis.GetEndPoints().First());
        }
        public async Task DeleteKeyAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public List<string> GetAllKeysAsync()
        {
            var keys = _server.Keys();
            return keys.Select(k => k.ToString()).ToList();
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task SetValueAsync(string key, string value)
        {
            await _database.StringSetAsync(key, value);
        }
    }
}
