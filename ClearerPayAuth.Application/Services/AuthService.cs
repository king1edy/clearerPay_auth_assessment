using ClearerPayAuth.Application.Auth;
using ClearerPayAuth.Application.DTOs;
using ClearerPayAuth.Application.Interfaces;
using ClearerPayAuth.Domain.Entities;
using ClearerPayAuth.Domain.Interfaces;

namespace ClearerPayAuth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly JwtTokenGenerator _jwtGenerator;

    public AuthService(IUserRepository userRepo, JwtTokenGenerator jwtGenerator)
    {
        _userRepo = userRepo;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<bool> RegisterAsync(RegisterUserDto dto)
    {
        try
        {
            var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
            if (existingUser != null) return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            string userName = $"{dto.FirstName} {dto.LastName}";
            var user = new User { FirstName = dto.FirstName, LastName = dto.LastName, Username = userName, Email = dto.Email, Password = hashedPassword };

            var sql = "INSERT INTO Users (FirstName, LastName, Username, Email, Password) VALUES (@FirstName, @LastName, @Username, @Email, @Password)";
            await _userRepo.AddAsync(sql, user);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string?> LoginAsync(LoginUserDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null || !_jwtGenerator.IsValidPassword(dto.Password, user.Password))
            return null;

        return _jwtGenerator.GenerateToken(user);
    }

    public async Task LogoutAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        throw new NotImplementedException();
    }
}