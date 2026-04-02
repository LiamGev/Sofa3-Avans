using Domain.Adapters;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Observers;
using Domain.States;
using Moq;
using Xunit;

namespace Tests.Domain
{
    public class ObserverTests
    {
        [Fact]
        public void Observer_Is_Notified_When_State_Changes()
        {
            var observerMock = new Mock<INotificationObserver>();
            var item = new BacklogItem("Test", "Description", "Story", new ToDoState());

            item.AttachObserver(observerMock.Object);

            item.StartWork();

            observerMock.Verify(
                o => o.Update(It.Is<string>(msg => msg.Contains("changed state to Doing"))),
                Times.Once);
        }

        [Fact]
        public void Observer_Is_Notified_When_Item_Moves_To_ReadyForTesting()
        {
            var observerMock = new Mock<INotificationObserver>();
            var item = new BacklogItem("Test", "Description", "Story", new ToDoState());

            item.AttachObserver(observerMock.Object);

            item.StartWork();
            item.MoveToReadyForTesting();

            observerMock.Verify(
                o => o.Update(It.Is<string>(msg => msg.Contains("Ready For Testing"))),
                Times.AtLeastOnce);
        }

        [Fact]
        public void Observer_Is_Notified_When_Testing_Is_Rejected()
        {
            var observerMock = new Mock<INotificationObserver>();
            var item = new BacklogItem("Test", "Description", "Story", new ToDoState());

            item.AttachObserver(observerMock.Object);

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.RejectTesting();

            observerMock.Verify(
                o => o.Update(It.Is<string>(msg => msg.Contains("Testing failed"))),
                Times.Once);
        }

        [Fact]
        public void EmailNotificationObserver_Sends_Message_To_EmailChannel()
        {
            var channel = new EmailChannel();
            var observer = new EmailNotificationObserver(channel);

            observer.Update("Item changed");

            Assert.Single(channel.SentMessages);
            Assert.Equal("EMAIL: Item changed", channel.SentMessages[0]);
        }

        [Fact]
        public void SlackNotificationObserver_Sends_Message_To_SlackChannel()
        {
            var channel = new SlackChannel();
            var observer = new SlackNotificationObserver(channel);

            observer.Update("Item changed");

            Assert.Single(channel.SentMessages);
            Assert.Equal("SLACK: Item changed", channel.SentMessages[0]);
        }
    }
}