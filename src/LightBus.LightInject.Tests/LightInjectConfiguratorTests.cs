namespace LightBus.LightInject.Tests
{
    using LightBus.LightInject;

    using Xunit;

    using global::LightInject;

    public class LightInjectConfiguratorTests
    {
        [Fact]
        public void When_configurating_bus_with_light_inject_should_send_messages()
        {
            var serviceContainer = new ServiceContainer();
            var lightInjectConfigurator = new LightInjectConfigurator(serviceContainer);
            lightInjectConfigurator.RegisterHandlersFrom(typeof(LightInjectConfiguratorTests).Assembly);
            var bus = new Bus(lightInjectConfigurator);
            
            Assert.DoesNotThrow(() => bus.Send(new TestCommand()));
            Assert.DoesNotThrow(() => bus.Publish(new TestEvent()));
            Assert.DoesNotThrow(() => ((ISendRequests)bus).Get(new TestRequest()));
        }
    }
}