namespace TaskManager.Infrastructure.MessageBroker.Interface;

public interface IEmailSender
{
    Task SendEmailAsync(string to, string subject, string body);
}
