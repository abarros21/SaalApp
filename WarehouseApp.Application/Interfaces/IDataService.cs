namespace WarehouseApp.Application.Interfaces
{
    public interface IDataService<T>
    {
        Task SaveAsync(string key, T data);
        Task<T> GetAsync(string key);
        Task<Dictionary<string,T>> GetAllAsync();
        Task DeleteAsync(string key);
    }
}
