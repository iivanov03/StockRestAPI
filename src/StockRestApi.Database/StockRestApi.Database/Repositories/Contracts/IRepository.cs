namespace StockRestApi.Database.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T> GetByIdAsync(int id);

        Task<bool> CreateAsync(T entity);

        Task<bool> UpdateAsync(T entity);

        Task<bool> DeleteAsync(int id);
    }
}
