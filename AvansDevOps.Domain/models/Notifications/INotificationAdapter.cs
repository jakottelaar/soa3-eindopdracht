namespace AvansDevOps.Domain.Models.Notifications;

public interface INotificationAdapter
{
    void SendNotification(string message, string receiver);
}