using HotChocolate.Types.Pagination;
using Microsoft.Extensions.DependencyInjection;
// using TaskManager.GraphQL.Queries;
// using TaskManager.GraphQL.Mutations;

namespace TaskManager.GraphQL;

public static class DependencyInjection
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            // .AddAuthorization()
            // .AddQueryType<Query>()
            // .AddMutationType<Mutation>()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .SetPagingOptions(new PagingOptions
            {
                MaxPageSize = 50,
                DefaultPageSize = 20,
                IncludeTotalCount = true
            });

        return services;
    }
}