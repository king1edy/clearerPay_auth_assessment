using System.ComponentModel.Design;
using ClearerPayAuth.Application.Interfaces;
using ClearerPayAuth.Application.Services;
using ClearerPayAuth.Domain.Interfaces;
using ClearerPayAuth.Infrastructure.Data;
using ClearerPayAuth.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ClearerPayAuth.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        // configure service
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthService, AuthService>();
    }
}