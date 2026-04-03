namespace AvansDevOps.Domain.Models.Activities;

public interface IActivityState
{
    void Start(Activity activity);
    void Complete(Activity activity);
}