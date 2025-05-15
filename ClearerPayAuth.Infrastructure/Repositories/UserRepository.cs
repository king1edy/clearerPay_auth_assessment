using ClearerPayAuth.Domain.Entities;
using ClearerPayAuth.Domain.Interfaces;
using ClearerPayAuth.Infrastructure.Data;
using Dapper;

namespace ClearerPayAuth.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task AddAsync(User user)
        {
            var sql = "INSERT INTO Users (Id, Email, PasswordHash) VALUES (@Id, @Email, @PasswordHash)";
            using var connection = _connectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, user);
        }
    }
}
