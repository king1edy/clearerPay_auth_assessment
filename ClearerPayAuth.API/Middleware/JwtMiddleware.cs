using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ClearerPayAuth.Application.Auth;
using Microsoft.IdentityModel.Tokens;

namespace ClearerPayAuth.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public JwtMiddleware(RequestDelegate next, JwtSettings jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = _jwtSettings.ValidIssuer,
                        ValidAudience = _jwtSettings.ValidAudience
                    }, out _);
                }
                catch
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }
            }

            await _next(context);
        }
    }
}