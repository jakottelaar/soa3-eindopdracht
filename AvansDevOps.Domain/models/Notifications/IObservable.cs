namespace AvansDevOps.Domain.Models.Notifications;

public interface IObservable
{
    void NotifyObservers(string message);
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
}
