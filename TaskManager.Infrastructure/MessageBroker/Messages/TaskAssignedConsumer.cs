using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TaskManager.Infrastructure.MessageBroker.Interface;
using TaskManager.Shared.Common;

namespace TaskManager.Infrastructure.MessageBroker.Messages;

public class TaskAssignedConsumer : RabbitMqConsumerBase<TaskAssignedEvent>
{
    private readonly IEmailSender _emailSender;

    public TaskAssignedConsumer(IServiceScopeFactory scopeFactory,
        IOptions<RabbitMqSettings> options,
        IEmailSender emailSender)
        : base(scopeFactory, options, "task.exchange", "task.queue", "task.assigned")
    {
        _emailSender = emailSender;
    }

    protected override async Task HandleMessageAsync(TaskAssignedEvent message)
    {
        var subject = "You have been assigned a new task";
        var body = $"Hello,\n\nYou have been assigned a task: \"{message.TaskTitle}\".\n\nRegards,\nTask Manager Team";
        
        await _emailSender.SendEmailAsync(message.AssigneeEmail, subject, body);
    }
}
