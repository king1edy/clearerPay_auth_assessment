using System.Data;
using ClearerPayAuth.Domain.Interfaces;
using ClearerPayAuth.Infrastructure.Data;
using Dapper;

namespace ClearerPayAuth.Infrastructure.Repositories;

public class Repository<T, TKey> : IRepository<T, TKey> where T : class
{
    protected readonly IDbConnection _connection;

    public Repository(IDbConnectionFactory factory)
    {
        _connection = factory.CreateConnection();
    }

    public async Task<T?> GetByIdAsync(TKey id)
    {
        var sql = $"SELECT * FROM [{typeof(T).Name}s] WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, new { Id = id });
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM [{typeof(T).Name}s]";
        return await _connection.QueryAsync<T>(sql);
    }

    public async Task<T?> FindAsync(string sql, object param)
    {
        return await _connection.QueryFirstOrDefaultAsync<T>(sql, param);
    }

    public async Task<int> AddAsync(string sql, object param)
    {
        return await _connection.ExecuteAsync(sql, param);
    }
}
