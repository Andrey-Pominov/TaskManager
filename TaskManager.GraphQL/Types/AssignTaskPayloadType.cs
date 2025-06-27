using TaskManager.Shared.Common;

namespace TaskManager.GraphQL.Types;

public class AssignTaskPayloadType : ObjectType<AssignTaskPayload>
{
    protected override void Configure(IObjectTypeDescriptor<AssignTaskPayload> descriptor)
    {
        descriptor
            .Field(p => p.Task)
            .Type<TaskType>();

        descriptor
            .Field(p => p.Error)
            .Type<StringType>();
    }
}