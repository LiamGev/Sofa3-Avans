using Domain.Adapters;
using Xunit;

namespace Tests.Domain
{
    public class ChannelTests
    {
        [Fact]
        public void EmailChannel_Stores_Sent_Message()
        {
            var channel = new EmailChannel();

            channel.Send("Test email");

            Assert.Single(channel.SentMessages);
            Assert.Equal("Test email", channel.SentMessages[0]);
        }

        [Fact]
        public void SlackChannel_Stores_Sent_Message()
        {
            var channel = new SlackChannel();

            channel.Send("Test slack");

            Assert.Single(channel.SentMessages);
            Assert.Equal("Test slack", channel.SentMessages[0]);
        }
    }
}