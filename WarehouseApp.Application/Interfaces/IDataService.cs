namespace WarehouseApp.Application.Interfaces
{
    public interface IDataService<T>
    {
        Task SaveAsync(string key, T data);
        Task<List<T>> GetAsync(string key);
        Task<List<T>> GetAllAsync();
    }
}
