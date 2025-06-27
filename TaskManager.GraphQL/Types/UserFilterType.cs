using HotChocolate.Data.Filters;
using TaskManager.Domain.Entities;

namespace TaskManager.GraphQL.Types;

public class UserFilterType : FilterInputType<User>
{
    protected override void Configure(IFilterInputTypeDescriptor<User> descriptor)
    {
        descriptor.BindFieldsExplicitly();
        descriptor.Field(u => u.Username);
        descriptor.Field(u => u.Email);
        descriptor.Field(u => u.Role);
    }
}
