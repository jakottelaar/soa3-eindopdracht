namespace AvansDevOps.Domain.Models.Notifications;

public interface IObserver
{
    void Update(string message);
}