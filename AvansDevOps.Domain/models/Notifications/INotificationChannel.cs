namespace AvansDevOps.Domain.Models.Notifications.Channels;

public interface INotificationChannel
{
    void Send(string message);
}
