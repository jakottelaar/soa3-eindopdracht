using AvansDevOps.Domain.Models.Notifications.Channels;

namespace AvansDevOps.Domain.Models.Notifications;

public class NotificationObserver : IObserver
{
    private readonly List<INotificationChannel> channels;

    public NotificationObserver(List<INotificationChannel> channels)
    {
        this.channels = channels;
    }

    public void Update(string message)
    {
        foreach (var channel in channels)
        {
            channel.Send(message);
        }
    }
}
