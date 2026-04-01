using App.Services;
using Domain.Entities;
using Infra.Repos;
using System;
using System.Linq;
using Xunit;

namespace Tests.Application
{
    public class BacklogServiceTests
    {
        [Fact]
        public void Constructor_WithNullRepository_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new BacklogService(null!));
        }

        [Fact]
        public void CreateStory_Adds_Item_To_Project()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateStory(project.Id, "New Story", "Description");

            Assert.Single(project.BacklogItems);
            Assert.Equal("Story", item.Type);
            Assert.Equal("New Story", item.Title);
            Assert.Equal("Description", item.Description);
        }

        [Fact]
        public void CreateStory_WithUnknownProject_ThrowsInvalidOperationException()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);

            Assert.Throws<InvalidOperationException>(() =>
                service.CreateStory(Guid.NewGuid(), "Story", "Description"));
        }

        [Fact]
        public void CreateBug_Adds_Bug_To_Project()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            var item = service.CreateBug(project.Id, "New Bug", "Bug Description");

            Assert.Single(project.BacklogItems);
            Assert.Equal("Bug", item.Type);
        }

        [Fact]
        public void StartWork_Changes_BacklogItem_State()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");

            service.StartWork(project.Id, item.Id);

            Assert.Equal("In Progress", item.CurrentState.Name);
        }

        [Fact]
        public void MoveToTesting_Changes_BacklogItem_State()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);

            service.MoveToTesting(project.Id, item.Id);

            Assert.Equal("Testing", item.CurrentState.Name);
        }

        [Fact]
        public void Complete_Changes_BacklogItem_State_To_Done()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);

            Assert.Throws<InvalidOperationException>(() =>
                service.MoveToTesting(project.Id, Guid.NewGuid()));
        }

        [Fact]
        public void Complete_Changes_BacklogItem_State_To_Done()
        {
            var repository = new InMemoryProjectRepository();
            var service = new BacklogService(repository);
            var project = new Project("Test project");

            repository.Add(project);
            var item = service.CreateStory(project.Id, "New Story", "Description");
            service.StartWork(project.Id, item.Id);
            service.MoveToTesting(project.Id, item.Id);

            service.Complete(project.Id, item.Id);

            Assert.Equal("Done", item.CurrentState.Name);
        }
    }
}