using ClearerPayAuth.Domain.Entities;
using ClearerPayAuth.Domain.Interfaces;
using ClearerPayAuth.Infrastructure.Data;
using Dapper;

namespace ClearerPayAuth.Infrastructure.Repositories
{
    public class UserRepository : Repository<User, long>, IUserRepository
    {
        public UserRepository(IDbConnectionFactory factory) : base(factory) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
        }
    }
}
