namespace ClearerPayAuth.Application.DTOs;

public class RegisterUserDto
{
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; }
}