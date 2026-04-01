namespace AvansDevOps.Domain.Models.Notifications;

public class NotificationObserver : IObserver
{
    private readonly INotificationAdapter adapter;
    private readonly string name;

    public NotificationObserver(INotificationAdapter adapter, string name)
    {
        this.adapter = adapter;
        this.name = name;
    }

    public void Update(string message)
    {
        adapter.SendNotification(message, name);
    }
}