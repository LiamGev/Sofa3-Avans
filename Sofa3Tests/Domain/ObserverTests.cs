using Domain.Entities;
using Domain.Interfaces;
using Domain.States;
using Moq;
using System.Timers;
using Xunit;

namespace Tests.Domain
{
    public class ObserverTests
    {
        [Fact]
        public void Observer_Is_Notified_When_State_Changes()
        {
            var observerMock = new Mock<INotificationObserver>();
            var item = new BacklogItem("Test", "Desc", "Story", new ToDoState());

            item.AttachObserver(observerMock.Object);
            item.StartWork();

            observerMock.Verify(
                o => o.Update(It.Is<string>(msg => msg.Contains("changed state to In Progress"))),
                Times.Once);
        }

        [Fact]
        public void Multiple_Observers_Are_Notified_When_State_Changes()
        {
            var observer1 = new Mock<INotificationObserver>();
            var observer2 = new Mock<INotificationObserver>();
            var item = new BacklogItem("Test", "Desc", "Story", new ToDoState());

            item.AttachObserver(observer1.Object);
            item.AttachObserver(observer2.Object);

            item.StartWork();

            observer1.Verify(o => o.Update(It.IsAny<string>()), Times.Once);
            observer2.Verify(o => o.Update(It.IsAny<string>()), Times.Once);
        }
    }
}