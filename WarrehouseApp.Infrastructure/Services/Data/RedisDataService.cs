using Newtonsoft.Json;
using WarehouseApp.Application.Interfaces;
using WarrehouseApp.Infrastructure.Data.Interfaces.Redis;

namespace WarrehouseApp.Infrastructure.Data.Services.Data
{
    public class RedisDataService<T>(IRedisRepository redisRepository) : IDataService<T>
    {
        private readonly IRedisRepository _redisRepository = redisRepository;

        public async Task<List<T>> GetAllAsync()
        {
            // Obtener todas las claves almacenadas en Redis
            var allKeys = _redisRepository.GetAllKeysAsync();

            // List para almacenar todos los datos
            var allData = new List<T>();

            // Recorrer todas las claves y obtener los datos asociados a cada una de ellas
            foreach (var key in allKeys)
            {
                var serializedData = await _redisRepository.GetValueAsync(key);
                if (!string.IsNullOrEmpty(serializedData))
                {
                    var data = JsonConvert.DeserializeObject<T>(serializedData);
                    allData.Add(data);
                }
            }

            return allData;
        }

        public async Task<List<T>> GetAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            var serializedData = await _redisRepository.GetValueAsync(key);
            if (string.IsNullOrEmpty(serializedData))
                return default;

            return JsonConvert.DeserializeObject<List<T>>(serializedData);
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
