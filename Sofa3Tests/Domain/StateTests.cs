using Domain.Entities;
using Domain.States;
using Xunit;

namespace Tests.Domain
{
    public class StateTests
    {
        [Fact]
        public void BacklogItem_Can_Move_From_ToDo_To_InProgress()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.StartWork();

            Assert.Equal("In Progress", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Can_Move_From_InProgress_To_Testing()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());
            item.StartWork();

            item.MoveToTesting();

            Assert.Equal("Testing", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Can_Move_From_Testing_To_Done()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());
            item.StartWork();
            item.MoveToTesting();

            item.Complete();

            Assert.Equal("Done", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Cannot_Complete_Directly_From_ToDo()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            Assert.Throws<InvalidOperationException>(() => item.Complete());
        }

        [Fact]
        public void BacklogItem_Cannot_Move_To_Testing_Directly_From_ToDo()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            Assert.Throws<InvalidOperationException>(() => item.MoveToTesting());
        }
    }
}