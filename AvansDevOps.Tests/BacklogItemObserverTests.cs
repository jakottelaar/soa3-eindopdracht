using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Notifications;
using NSubstitute;

namespace AvansDevOps.Tests;

public class BacklogItemObserverTests
{
     [Fact]
    public void MoveToReadyForTesting_ShouldNotify_RegisteredObserver()
    {
        var mockAdapter = Substitute.For<INotificationAdapter>();
        var observer = new NotificationObserver(mockAdapter, "TestObserver");
        var backlogItem = new BacklogItem { Title = "Test Backlog Item", State = new BacklogItemDoingState() };
        backlogItem.Subscribe(observer);
        backlogItem.MarkReadyForTesting();
        mockAdapter.Received(1).SendNotification("BacklogItem 'Test Backlog Item' is now ready for testing.", "TestObserver");
    }
}