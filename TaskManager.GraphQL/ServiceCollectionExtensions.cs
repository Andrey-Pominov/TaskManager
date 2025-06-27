using Microsoft.Extensions.DependencyInjection;
using TaskManager.Domain.Entities;
using TaskManager.GraphQL.Mutations;
using TaskManager.GraphQL.Queries;
using TaskManager.GraphQL.Types;
using TaskManager.Shared.Common;

namespace TaskManager.GraphQL;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGraphQLServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddHttpRequestInterceptor<CustomRequestInterceptor>()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true)
            .AddProjections()
            .AddFiltering() 
            .AddType<TaskFilterType>()
            .AddType<UserFilterType>()
            .AddType(new EnumType<Status>(d =>
            {
                d.Name("Status");
                d.BindValuesExplicitly();
                d.Item(Status.TODO);
                d.Item(Status.IN_PROGRESS);
                d.Item(Status.DONE);
            }))
            .AddDirectiveType<SkipDirectiveType>() 
            .AddDirectiveType<IncludeDirectiveType>()
            .AddQueryType<RootQuery>()
            .AddMutationType<RootMutation>()
            .AddType<EnumType<Status>>()
            .AddType<AuthMutation>()
            .AddType<TaskType>()
            .AddType<UserType>()
            .AddType<AssignTaskPayloadType>()
            .AddType<TaskMutation>()
            .AddSorting();

        return services;
    }
}