namespace AvansDevOps.Domain.Models.Notifications;

public interface IObservable
{
    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);
    void NotifyObservers(string message);
}