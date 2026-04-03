using AvansDevOps.Domain.Models.Notifications;

namespace AvansDevOps.Domain.Models.Pipeline;

public class Pipeline : PipelineComponent, IObservable
{
    private readonly List<IPipelineComponent> steps = [];
    private readonly List<IObserver> observers = [];

    public Pipeline(string name) : base(name) { }
    public void AddStep(IPipelineComponent step) => steps.Add(step);
    public void RemoveStep(IPipelineComponent step) => steps.Remove(step);

    public void Subscribe(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Unsubscribe(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string message)
    {
        foreach (var observer in observers)
        {
            observer.Update(message);
        }
    }

    public override bool Execute()
    {
        foreach (var step in steps)
        {
            if (!step.Execute())
            {
                NotifyObservers($"Pipeline '{Name}' failed at step '{step.GetType().Name}'.");
                return false;
            }
        }

        return true;
    }
}
