using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Interfaces;

namespace TaskManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddScoped<ITaskService, TaskService>();
        // services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
