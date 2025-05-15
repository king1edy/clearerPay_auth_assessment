using ClearerPayAuth.Application.DTOs;

namespace ClearerPayAuth.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterUserDto dto);
        Task<string?> LoginAsync(LoginUserDto dto);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
    }
}
