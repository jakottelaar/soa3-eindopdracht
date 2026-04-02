namespace AvansDevOps.Domain.Models.Notifications;

public class NotificationSlackAdapter : INotificationAdapter
{
    public void SendNotification(string message, string receiver)
    {
        Console.WriteLine($"Sending Slack notification to {receiver}: {message}");
    }
}