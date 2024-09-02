namespace Metafar.Banking.Application.Interfaces.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        #region Async
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        #endregion
    }
}
