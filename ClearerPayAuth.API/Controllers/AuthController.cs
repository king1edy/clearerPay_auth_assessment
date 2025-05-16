using ClearerPayAuth.Application.DTOs;
using ClearerPayAuth.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClearerPayAuth.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var success = await _authService.RegisterAsync(dto);
            return success ? Ok("User registered") : BadRequest("Email already exists");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            return token == null ? Unauthorized("Invalid credentials") : Ok(new { token });
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure() => Ok("Authenticated access!");
    }
}
