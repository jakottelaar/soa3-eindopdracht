using AvansDevOps.Domain.Models.Notifications.Channels;

namespace AvansDevOps.Domain.Models.Notifications;

public class NotificationObserver : IObserver
{
    private readonly List<INotificationChannel> _channels;

    public NotificationObserver(List<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public void Update(string message)
    {
        foreach (var channel in _channels)
        {
            channel.Send(message);
        }
    }
}
