using TaskManager.Domain.Entities;

namespace TaskManager.GraphQL.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Field(u => u.Id)
            .Type<NonNullType<IdType>>();

        descriptor
            .Field(u => u.Username)
            .Type<NonNullType<StringType>>()
            .UseFiltering();

        descriptor
            .Field(u => u.Email)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(u => u.Role)
            .Type<NonNullType<StringType>>();

        descriptor
            .Field(u => u.PasswordHash)
            .Ignore();

        descriptor
            .Field(u => u.AssignedTasks)
            .Type<ListType<TaskType>>()
            .Authorize("Admin");
    }
}