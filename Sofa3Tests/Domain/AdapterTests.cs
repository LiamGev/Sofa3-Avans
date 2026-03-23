using Domain.Adapters;
using Xunit;

namespace Tests.Domain
{
    public class AdapterTests
    {
        [Fact]
        public void LegacyEmailAdapter_Forwards_Message_To_Legacy_Service()
        {
            var legacyService = new LegacyEmailService();
            var adapter = new LegacyEmailAdapter(legacyService);

            adapter.Update("Test message");

            Assert.Single(legacyService.LegacyMessages);
            Assert.Equal("Test message", legacyService.LegacyMessages[0]);
        }
    }
}