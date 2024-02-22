namespace WarrehouseApp.Infrastructure.Data.Interfaces.Redis
{
    public interface IRedisRepository
    {
        Task<string> GetValueAsync(string key);
        Task SetValueAsync(string key, string value);
        Task DeleteKeyAsync(string key);
        List<string> GetAllKeysAsync();
    }
}
