using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using TaskManager.Infrastructure.Persistence;
// using TaskManager.Infrastructure.Auth;
// using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        // services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<ITaskRepository, TaskRepository>();

        // JWT setup
        // var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();
        // services.AddSingleton(jwtSettings);

        return services;
    }
}