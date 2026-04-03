using AvansDevOps.Domain.Models.BacklogItems;
using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;
using NSubstitute;

namespace AvansDevOps.Tests;

public class BacklogItemObserverTests
{
    [Fact]
    public void MoveToReadyForTesting_ShouldNotify_RegisteredObserver()
    {
        var mockChannel = Substitute.For<INotificationChannel>();
        var channels = new List<INotificationChannel> { mockChannel };
        var observer = new NotificationObserver(channels);
        var backlogItem = new BacklogItem { Title = "Test Backlog Item", State = new BacklogItemDoingState() };
        backlogItem.Subscribe(observer);
        backlogItem.MarkReadyForTesting();
        mockChannel.Received(1).Send(Arg.Any<string>());
    }
}