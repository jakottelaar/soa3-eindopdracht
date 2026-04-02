namespace AvansDevOps.Domain.Models.Notifications;

public class NotificationEmailAdapter : INotificationAdapter
{
    public void SendNotification(string message, string receiver)
    {
        Console.WriteLine($"Sending email notification to {receiver}: {message}");
    }
}