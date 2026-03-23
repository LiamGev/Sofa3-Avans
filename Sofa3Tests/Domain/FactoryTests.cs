using Domain.Factories;
using Xunit;

namespace Tests.Domain
{
    public class FactoryTests
    {
        [Fact]
        public void StoryFactory_Creates_Story_Item()
        {
            var factory = new StoryFactory();

            var item = factory.Create("Story title", "Story description");

            Assert.Equal("Story", item.Type);
            Assert.Equal("To Do", item.CurrentState.Name);
            Assert.Equal("Story title", item.Title);
        }

        [Fact]
        public void BugFactory_Creates_Bug_Item()
        {
            var factory = new BugFactory();

            var item = factory.Create("Bug title", "Bug description");

            Assert.Equal("Bug", item.Type);
            Assert.Equal("To Do", item.CurrentState.Name);
            Assert.Equal("Bug title", item.Title);
        }
    }
}