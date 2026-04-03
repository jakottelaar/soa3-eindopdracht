using AvansDevOps.Domain.Models.BacklogItems.States;
using AvansDevOps.Domain.Models.Discussion;
using AvansDevOps.Domain.Models.Discussion.Composite;
using AvansDevOps.Domain.Models.Notifications;
using AvansDevOps.Domain.Models.Notifications.Channels;
using NSubstitute;

namespace AvansDevOps.Tests;

public class ForumTests
{
    //TC-32:
    [Fact]
    public void CreatesThreadForNonDoneBacklogItem()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var item = TestFactory.CreateBacklogItem("Item 34");
        var discussion = new Discussion(item, author);
        var thread = new DiscussionThread("Thread message", author);

        discussion.AddThread(thread);

        Assert.Contains(thread, discussion.Root.Children);
    }

    //TC-33:
    [Fact]
    public void AddsRepliesToThread()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var post = new DiscussionPost("Initial post", author);
        var reply = new DiscussionPost("Reply", author);

        post.AddReply(reply);

        Assert.Single(post.Replies);
        Assert.Equal("Reply", post.Replies[0].Message);
    }

    //TC-34:
    [Fact]
    public void SendsNotificationOnNewForumReply()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var item = TestFactory.CreateBacklogItem("Item 36");
        var discussion = new Discussion(item, author);
        var channel = Substitute.For<INotificationChannel>();
        var observer = new NotificationObserver([channel]);
        discussion.Subscribe(observer);

        discussion.AddPost(new DiscussionPost("new reply", author));

        channel.Received(1).Send(Arg.Is<string>(m => m.Contains("New Reply in Discussion") && m.Contains("Item 36")));
    }

    //TC-35:
    [Fact]
    public void LocksThreadsWhenBacklogItemIsDone()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var item = TestFactory.CreateBacklogItem("Item 37");
        item.State = new BacklogItemDoneState();

        var discussion = new Discussion(item, author);

        Assert.True(discussion.IsLocked());
    }

    //TC-36:
    [Fact]
    public void PreventsNewRepliesWhenLocked()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var item = TestFactory.CreateBacklogItem("Item 38");
        item.State = new BacklogItemDoneState();
        var discussion = new Discussion(item, author);

        Assert.Throws<InvalidOperationException>(() => discussion.AddPost(new DiscussionPost("blocked", author)));
    }

    //TC-37:
    [Fact]
    public void PreventsThreadCreationForDoneItem()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var item = TestFactory.CreateBacklogItem("Item 39");
        item.State = new BacklogItemDoneState();
        var discussion = new Discussion(item, author);

        Assert.Throws<InvalidOperationException>(() => discussion.AddThread(new DiscussionThread("not allowed", author)));
    }

    //TC-38:
    [Fact]
    public void LocksThreadsImmediatelyWhenItemTransitionsToDone()
    {
        var author = TestFactory.CreateDeveloper("dev");
        var item = TestFactory.CreateBacklogItem("Item 40");
        var discussion = new Discussion(item, author);

        Assert.False(discussion.IsLocked());

        item.State = new BacklogItemDoneState();

        Assert.True(discussion.IsLocked());
    }
}
