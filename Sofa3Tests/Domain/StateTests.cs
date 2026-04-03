using Domain.Entities;
using Domain.States;
using Xunit;

namespace Tests.Domain
{
    // State pattern tests:
    // Deze tests controleren of alleen geldige statusovergangen worden toegestaan
    // en of ongeldige overgangen correct worden geblokkeerd.
    public class StateTests
    {
        [Fact]
        public void BacklogItem_Can_Move_From_ToDo_To_Doing()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.StartWork();

            Assert.Equal("Doing", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Can_Move_From_Doing_To_ReadyForTesting()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());
            item.StartWork();

            item.MoveToReadyForTesting();

            Assert.Equal("Ready For Testing", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Can_Move_From_ReadyForTesting_To_Testing()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());
            item.StartWork();
            item.MoveToReadyForTesting();

            item.StartTesting();

            Assert.Equal("Testing", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Can_Move_From_Testing_To_Tested()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());
            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();

            item.ApproveTesting();

            Assert.Equal("Tested", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Can_Move_From_Tested_To_Done_When_All_Activities_Are_Completed()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.AddActivity("Implement feature");
            var activity = item.Activities.First();
            item.CompleteActivity(activity.Id);

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.ApproveTesting();

            item.ApproveDone();

            Assert.Equal("Done", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_Cannot_ApproveDone_Without_Completed_Activities()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.AddActivity("Implement feature");

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.ApproveTesting();

            Assert.Throws<InvalidOperationException>(() => item.ApproveDone());
        }

        [Fact]
        public void BacklogItem_Cannot_Move_To_ReadyForTesting_Directly_From_ToDo()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            Assert.Throws<InvalidOperationException>(() => item.MoveToReadyForTesting());
        }

        [Fact]
        public void BacklogItem_Cannot_StartTesting_Directly_From_ToDo()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            Assert.Throws<InvalidOperationException>(() => item.StartTesting());
        }

        [Fact]
        public void BacklogItem_RejectTesting_Moves_Back_To_ToDo()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();

            item.RejectTesting();

            Assert.Equal("To Do", item.CurrentState.Name);
        }

        [Fact]
        public void BacklogItem_RejectDone_Moves_Back_To_ReadyForTesting()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.ApproveTesting();

            item.RejectDone();

            Assert.Equal("Ready For Testing", item.CurrentState.Name);
        }

        [Fact]
        public void DoneState_RejectDone_Moves_Item_Back_To_ToDo()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.AddActivity("Implement feature");
            var activity = item.Activities.First();
            item.CompleteActivity(activity.Id);

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.ApproveTesting();
            item.ApproveDone();

            item.RejectDone();

            Assert.Equal("To Do", item.CurrentState.Name);
        }

        [Fact]
        public void DoneState_Start_ThrowsInvalidOperationException()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.AddActivity("Implement feature");
            var activity = item.Activities.First();
            item.CompleteActivity(activity.Id);

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.ApproveTesting();
            item.ApproveDone();

            Assert.Throws<InvalidOperationException>(() => item.StartWork());
        }

        [Fact]
        public void BacklogItem_CanHave_AtMostOneAssignedDeveloper()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.AssignDeveloper("Alice");

            Assert.Throws<InvalidOperationException>(() => item.AssignDeveloper("Bob"));
        }

        [Fact]
        public void CompletedBacklogItem_LocksDiscussion()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.AddActivity("Implement feature");
            var activity = item.Activities.First();
            item.CompleteActivity(activity.Id);

            item.StartWork();
            item.MoveToReadyForTesting();
            item.StartTesting();
            item.ApproveTesting();
            item.ApproveDone();
            item.MarkAsCompleted();

            Assert.True(item.IsDiscussionLocked);
            Assert.Throws<InvalidOperationException>(() => item.AddDiscussionThread("Alice", "New thread after completion"));
        }

        [Fact]
        public void BacklogItem_Can_LinkCommit_AndBranch()
        {
            var item = new BacklogItem("Test item", "Description", "Story", new ToDoState());

            item.LinkCommit("abc123");
            item.LinkBranch("feature/test-item");

            Assert.Contains("abc123", item.LinkedCommits);
            Assert.Contains("feature/test-item", item.LinkedBranches);
        }
    }
}