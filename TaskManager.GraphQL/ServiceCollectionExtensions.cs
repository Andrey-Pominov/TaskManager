using HotChocolate.Data.Filters;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Entities;
using TaskManager.GraphQL.Mutations;
using TaskManager.GraphQL.Queries;
using TaskManager.GraphQL.Types;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<RootMutation>()
            .AddType<EnumType<Status>>()
            .AddType<AuthMutation>()
            .AddType<TaskType>()
            .AddType<UserType>()
            .AddType<AssignTaskPayloadType>()
            .AddType<FilterInputType<Task>>()
            .AddType<TaskMutation>()
            .AddFiltering() 
            .AddSorting();

        return services;
    }
}