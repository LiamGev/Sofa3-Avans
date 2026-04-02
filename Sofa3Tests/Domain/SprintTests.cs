using Domain.Entities;
using Domain.Strategies;
using Xunit;

namespace Tests.Domain
{
    public class SprintTests
    {
        [Fact]
        public void Constructor_WithValidData_CreatesSprintInCreatedState()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            Assert.Equal("Sprint 1", sprint.Name);
            Assert.Equal("Release", sprint.SprintType);
            Assert.Equal("Created", sprint.CurrentState.Name);
        }

        [Fact]
        public void Constructor_WithEmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Sprint("", new DateTime(2025, 1, 1), new DateTime(2025, 1, 14), "Release"));
        }

        [Fact]
        public void Constructor_WithInvalidDates_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new Sprint("Sprint 1", new DateTime(2025, 1, 14), new DateTime(2025, 1, 1), "Release"));
        }

        [Fact]
        public void AddBacklogItem_InCreatedState_AddsItem()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            var item = new BacklogItem("Story", "Description", "Story", new Domain.States.ToDoState());

            sprint.AddBacklogItem(item);

            Assert.Single(sprint.BacklogItems);
        }

        [Fact]
        public void AddBacklogItem_OutsideCreatedState_ThrowsInvalidOperationException()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.StartSprint();

            var item = new BacklogItem("Story", "Description", "Story", new Domain.States.ToDoState());

            Assert.Throws<InvalidOperationException>(() => sprint.AddBacklogItem(item));
        }

        [Fact]
        public void SetPipeline_AssignsPipeline()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            var pipeline = new Pipeline("Release Pipeline", new ReleasePipelineStrategy());

            sprint.SetPipeline(pipeline);

            Assert.NotNull(sprint.Pipeline);
            Assert.Equal("Release Pipeline", sprint.Pipeline!.Name);
        }

        [Fact]
        public void UploadReviewSummary_WithValidSummary_SetsSummary()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Review");

            sprint.UploadReviewSummary("Review completed successfully.");

            Assert.Equal("Review completed successfully.", sprint.ReviewSummary);
        }

        [Fact]
        public void UploadReviewSummary_WithEmptySummary_ThrowsArgumentException()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Review");

            Assert.Throws<ArgumentException>(() => sprint.UploadReviewSummary(""));
        }

        [Fact]
        public void Sprint_CanMove_FromCreated_ToActive_ToFinished_ToClosed()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Review");

            sprint.StartSprint();
            Assert.Equal("Active", sprint.CurrentState.Name);

            sprint.FinishSprint();
            Assert.Equal("Finished", sprint.CurrentState.Name);

            sprint.CloseSprint();
            Assert.Equal("Closed", sprint.CurrentState.Name);
        }

        [Fact]
        public void Sprint_CanMove_FromFinished_ToReleasing_WhenPipelineExists()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Pipeline", new ReleasePipelineStrategy()));
            sprint.StartSprint();
            sprint.FinishSprint();

            sprint.StartRelease();

            Assert.Equal("Releasing", sprint.CurrentState.Name);
        }

        [Fact]
        public void StartRelease_WithoutPipeline_ThrowsInvalidOperationException()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.StartSprint();
            sprint.FinishSprint();

            Assert.Throws<InvalidOperationException>(() => sprint.StartRelease());
        }

        [Fact]
        public void ReleasingSprint_CanBeCancelled()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Pipeline", new ReleasePipelineStrategy()));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.StartRelease();

            sprint.CancelRelease();

            Assert.Equal("Cancelled", sprint.CurrentState.Name);
        }

        [Fact]
        public void ReleasingSprint_CanBeClosed()
        {
            var sprint = new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");

            sprint.SetPipeline(new Pipeline("Pipeline", new ReleasePipelineStrategy()));
            sprint.StartSprint();
            sprint.FinishSprint();
            sprint.StartRelease();

            sprint.CloseSprint();

            Assert.Equal("Closed", sprint.CurrentState.Name);
        }
    }
}