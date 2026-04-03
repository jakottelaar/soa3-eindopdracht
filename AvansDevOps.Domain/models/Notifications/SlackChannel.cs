namespace AvansDevOps.Domain.Models.Notifications.Channels;

public class SlackChannel : INotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"[SLACK NOTIFICATION]\n{message}\n");
    }
}