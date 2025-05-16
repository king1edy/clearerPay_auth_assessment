namespace ClearerPayAuth.Domain.Interfaces;

public interface IRepository<T, TKey> where T : class
{
    Task<T?> GetByIdAsync(TKey id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> FindAsync(string sql, object param);
    Task<int> AddAsync(string sql, object param);
}
