﻿using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddScoped<ITaskService, TaskService>();
        // services.AddScoped<IUserService, UserService>();
        return services;
    }
}
