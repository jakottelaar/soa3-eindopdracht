using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;
using NSubstitute;

namespace AvansDevOps.Tests;

public class NotificationTests
{
    //TC-28:
    [Fact]
    public void SupportsEmailAndSlackChannels()
    {
        var emailChannel = new EmailChannel();
        var slackChannel = new SlackChannel();

        var exception = Record.Exception(() =>
        {
            emailChannel.Send("hello email");
            slackChannel.Send("hello slack");
        });

        Assert.Null(exception);
    }

    //TC-29:
    [Fact]
    public void SupportsMultipleChannelsAtTheSameTime()
    {
        var emailChannel = Substitute.For<INotificationChannel>();
        var slackChannel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([emailChannel, slackChannel]);

        observer.Update("multichannel message");

        emailChannel.Received(1).Send("multichannel message");
        slackChannel.Received(1).Send("multichannel message");
    }

    //TC-30:
    [Fact]
    public void SendsNotificationsToMultipleObservers()
    {
        var channelOne = Substitute.For<INotificationChannel>();
        var channelTwo = Substitute.For<INotificationChannel>();
        var observerOne = new NotificationObserver([channelOne]);
        var observerTwo = new NotificationObserver([channelTwo]);
        var item = TestFactory.CreateBacklogItem("Item 32");

        item.Subscribe(observerOne);
        item.Subscribe(observerTwo);

        item.NotifyObservers("broadcast message");

        channelOne.Received(1).Send("broadcast message");
        channelTwo.Received(1).Send("broadcast message");
    }

    //TC-31:
    [Fact]
    public void DoesNotSendNotificationWhenNoSubscribersExist()
    {
        var channel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([channel]);
        var item = TestFactory.CreateBacklogItem("Item 33");

        _ = observer;

        item.NotifyObservers("no receivers");

        channel.DidNotReceiveWithAnyArgs().Send(default!);
    }
}