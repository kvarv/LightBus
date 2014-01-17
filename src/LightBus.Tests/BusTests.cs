namespace LightBus.Tests
{
    using LightBus;

    using Should;

    using Xunit;

    public class BusTests
    {
        [Fact]
        public void Should_dispatch_command()
        {
            ISendCommands bus = Bus.Create(typeof(BusTests).Assembly);
            var command = new TestCommand();
            
            bus.Send(command);
            
            command.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void Should_dispatch_event()
        {
            IPublishEvents bus = Bus.Create(typeof(BusTests).Assembly);
            var message = new TestEvent();
            
            bus.Publish(message);
            
            message.NumberOfTimesHandled.ShouldEqual(2);
        }
    }
}