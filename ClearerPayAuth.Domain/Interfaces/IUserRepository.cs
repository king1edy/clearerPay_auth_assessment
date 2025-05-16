using ClearerPayAuth.Domain.Entities;

namespace ClearerPayAuth.Domain.Interfaces;

public interface IUserRepository : IRepository<User, long>
{
    Task<User?> GetByEmailAsync(string email);
}