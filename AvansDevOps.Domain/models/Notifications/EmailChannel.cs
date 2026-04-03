namespace AvansDevOps.Domain.Models.Notifications.Channels;

public class EmailChannel : INotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"[EMAIL NOTIFICATION]\n{message}\n");
    }
}
