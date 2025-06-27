using HotChocolate.Data.Filters;
using Task = TaskManager.Domain.Entities.Task;

namespace TaskManager.GraphQL.Types;

public class TaskFilterType : FilterInputType<Task>
{
    protected override void Configure(IFilterInputTypeDescriptor<Task> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Ignore(t => t.Id); 
        descriptor.Field(t => t.Title);
        descriptor.Field(t => t.Status);
        descriptor.Field(t => t.CreatedAt);

        descriptor.Field(t => t.CreatedBy).Type<UserFilterType>();
        descriptor.Field(t => t.AssignedTo).Type<UserFilterType>();
    }
}
