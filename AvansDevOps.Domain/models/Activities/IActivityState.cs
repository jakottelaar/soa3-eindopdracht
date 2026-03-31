namespace AvansDevOps.Domain.Models.Activities;

public interface IActivityState
{
    void Next(Activity context);
    void Previous(Activity context);
}