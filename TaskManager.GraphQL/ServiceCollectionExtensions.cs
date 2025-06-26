using Microsoft.Extensions.DependencyInjection;
using TaskManager.GraphQL.Mutations;
using TaskManager.GraphQL.Queries;

namespace TaskManager.GraphQL;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>();
        return services;
    }
}