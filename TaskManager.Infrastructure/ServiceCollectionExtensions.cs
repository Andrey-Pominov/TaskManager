using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Infrastructure.Auth;
using TaskManager.Infrastructure.Interfaces;
using TaskManager.Infrastructure.Repository;
using TaskManager.Shared.Configuration;

namespace TaskManager.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserRepository, UserRepository>();
        var jwtSettings = config.GetSection("JwtSettings").Get<JwtSettings>();
        services.AddSingleton(jwtSettings);

        return services;
    }
}