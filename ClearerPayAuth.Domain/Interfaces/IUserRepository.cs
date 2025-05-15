using ClearerPayAuth.Domain.Entities;

namespace ClearerPayAuth.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
}