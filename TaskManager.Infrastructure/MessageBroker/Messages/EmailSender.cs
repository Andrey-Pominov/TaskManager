using TaskManager.Infrastructure.MessageBroker.Interface;

namespace TaskManager.Infrastructure.MessageBroker.Messages;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}:\n{subject}\n{body}");
        return Task.CompletedTask;
    }
}
