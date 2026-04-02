using Domain.Entities;
using Domain.SprintStates;
using Domain.Strategies;
using Xunit;

namespace Tests.Domain
{
    public class SprintStateTests
    {
        private static Sprint CreateSprint()
        {
            return new Sprint(
                "Sprint 1",
                new DateTime(2025, 1, 1),
                new DateTime(2025, 1, 14),
                "Release");
        }

        [Fact]
        public void CreatedSprintState_FinishSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new CreatedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.FinishSprint(sprint));
        }

        [Fact]
        public void CreatedSprintState_StartRelease_Throws()
        {
            var sprint = CreateSprint();
            var state = new CreatedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartRelease(sprint));
        }

        [Fact]
        public void CreatedSprintState_CancelRelease_Throws()
        {
            var sprint = CreateSprint();
            var state = new CreatedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.CancelRelease(sprint));
        }

        [Fact]
        public void CreatedSprintState_CloseSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new CreatedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.CloseSprint(sprint));
        }

        [Fact]
        public void ActiveSprintState_StartSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new ActiveSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartSprint(sprint));
        }

        [Fact]
        public void ActiveSprintState_StartRelease_Throws()
        {
            var sprint = CreateSprint();
            var state = new ActiveSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartRelease(sprint));
        }

        [Fact]
        public void ActiveSprintState_CancelRelease_Throws()
        {
            var sprint = CreateSprint();
            var state = new ActiveSprintState();

            Assert.Throws<InvalidOperationException>(() => state.CancelRelease(sprint));
        }

        [Fact]
        public void ActiveSprintState_CloseSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new ActiveSprintState();

            Assert.Throws<InvalidOperationException>(() => state.CloseSprint(sprint));
        }

        [Fact]
        public void FinishedSprintState_StartSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new FinishedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartSprint(sprint));
        }

        [Fact]
        public void FinishedSprintState_FinishSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new FinishedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.FinishSprint(sprint));
        }

        [Fact]
        public void FinishedSprintState_CancelRelease_Throws()
        {
            var sprint = CreateSprint();
            var state = new FinishedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.CancelRelease(sprint));
        }

        [Fact]
        public void FinishedSprintState_CanStartRelease_WhenPipelineExists()
        {
            var sprint = CreateSprint();
            sprint.SetPipeline(new Pipeline("Pipeline", new ReleasePipelineStrategy()));
            var state = new FinishedSprintState();

            state.StartRelease(sprint);

            Assert.Equal("Releasing", sprint.CurrentState.Name);
        }

        [Fact]
        public void ReleasingSprintState_StartSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new ReleasingSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartSprint(sprint));
        }

        [Fact]
        public void ReleasingSprintState_FinishSprint_Throws()
        {
            var sprint = CreateSprint();
            var state = new ReleasingSprintState();

            Assert.Throws<InvalidOperationException>(() => state.FinishSprint(sprint));
        }

        [Fact]
        public void ReleasingSprintState_StartRelease_Throws()
        {
            var sprint = CreateSprint();
            var state = new ReleasingSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartRelease(sprint));
        }

        [Fact]
        public void ClosedSprintState_AllOperationsThrow()
        {
            var sprint = CreateSprint();
            var state = new ClosedSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartSprint(sprint));
            Assert.Throws<InvalidOperationException>(() => state.FinishSprint(sprint));
            Assert.Throws<InvalidOperationException>(() => state.StartRelease(sprint));
            Assert.Throws<InvalidOperationException>(() => state.CancelRelease(sprint));
            Assert.Throws<InvalidOperationException>(() => state.CloseSprint(sprint));
        }

        [Fact]
        public void CancelledSprintState_AllOperationsThrow()
        {
            var sprint = CreateSprint();
            var state = new CancelledSprintState();

            Assert.Throws<InvalidOperationException>(() => state.StartSprint(sprint));
            Assert.Throws<InvalidOperationException>(() => state.FinishSprint(sprint));
            Assert.Throws<InvalidOperationException>(() => state.StartRelease(sprint));
            Assert.Throws<InvalidOperationException>(() => state.CancelRelease(sprint));
            Assert.Throws<InvalidOperationException>(() => state.CloseSprint(sprint));
        }
    }
}