using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ClearerPayAuth.API.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddAPIService(this IServiceCollection services, IConfiguration configuration)
    {
        // configure service
        services.AddAuthorization();
        services.AddAuthentication(options => 
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                var key = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]);
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:validIssuer"],
                    ValidAudience = configuration["Jwt:validAudience"],
                    ClockSkew = TimeSpan.Zero // No clock skew for testing
                };
            });
        
        return services;
    }
}