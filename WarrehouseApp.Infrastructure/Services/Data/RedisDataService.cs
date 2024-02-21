using Newtonsoft.Json;
using WarehouseApp.Application.Interfaces;
using WarrehouseApp.Infrastructure.Data.Interfaces.Redis;

namespace WarrehouseApp.Infrastructure.Data.Services.Data
{
    public class RedisDataService<T>(IRedisRepository redisRepository) : IDataService<T>
    {
        private readonly IRedisRepository _redisRepository = redisRepository;

        public async Task DeleteAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            await _redisRepository.DeleteKeyAsync(key);
        }

        public async Task<Dictionary<string, T>> GetAllAsync()
        {
            
            var allKeys = _redisRepository.GetAllKeysAsync();

            
            var allData = new Dictionary<string, T>();

            
            foreach (var key in allKeys)
            {
                var serializedData = await _redisRepository.GetValueAsync(key);
                if (!string.IsNullOrEmpty(serializedData))
                {
                    var data = JsonConvert.DeserializeObject<T>(serializedData);
                    allData[key] = data;
                }
            }

            return allData;
        }

        public async Task<T> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            var serializedData = await _redisRepository.GetValueAsync(key);
            if (string.IsNullOrEmpty(serializedData))
                return default;

            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public async Task SaveAsync(string key, T data)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            var serializedData = JsonConvert.SerializeObject(data);
            await _redisRepository.SetValueAsync(key, serializedData);
        }
    }
}
