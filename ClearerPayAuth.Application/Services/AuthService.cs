using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClearerPayAuth.Application.DTOs;
using ClearerPayAuth.Application.Interfaces;
using ClearerPayAuth.Domain.Entities;
using ClearerPayAuth.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ClearerPayAuth.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IConfiguration _config;

    public AuthService(IUserRepository userRepo, IConfiguration config)
    {
        _userRepo = userRepo;
        _config = config;
    }

    public async Task<bool> RegisterAsync(RegisterUserDto dto)
    {
        var existingUser = await _userRepo.GetByEmailAsync(dto.Email);
        if (existingUser != null) return false;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        string userName = $"{dto.FirstName} {dto.LastName}";
        var user = new User { FirstName = dto.FirstName, LastName = dto.LastName, Username = userName, Email = dto.Email, Password = hashedPassword };
        
        var sql = "INSERT INTO Users (FirstName, LastName, UserName, Email, Password) VALUES (@FirstName, @UserName, @LastName, @Email, @Password)";
        await _userRepo.AddAsync(sql, user);
        return true;
    }
    public async Task<string?> LoginAsync(LoginUserDto dto)
    {
        var user = await _userRepo.GetByEmailAsync(dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config["Jwt:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
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