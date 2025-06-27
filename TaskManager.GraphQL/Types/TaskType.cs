using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL.Types;

public class TaskType : ObjectType<Task>
{
    protected override void Configure(IObjectTypeDescriptor<Task> descriptor)
    {
        descriptor
            .Field(t => t.Id)
            .Type<NonNullType<IdType>>(); 

        descriptor
            .Field(t => t.Title)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(t => t.Description)
            .Type<StringType>();

        descriptor
            .Field(t => t.Status)
            .Resolve(ctx => ctx.Parent<Task>().Status.ToString())
            .Type<NonNullType<StringType>>()
            .UseFiltering();

        descriptor
            .Field(t => t.CreatedAt)
            .Type<NonNullType<DateTimeType>>();

        descriptor
            .Field(t => t.CreatedBy)
            .Type<UserType>()
            .Authorize("Admin"); 
    }
}