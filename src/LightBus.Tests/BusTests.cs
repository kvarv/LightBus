namespace LightBus.Tests
{
    using System;
    using Should;
    using Xunit;

    using global::LightInject;

    public class BusTests
    {
        [Fact]
        public void When_sending_a_command_and_there_is_only_one_command_handler_should_invoke_command_handler()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleMessages<TestCommand>,TestCommandHandler>();
            var bus = new Bus(serviceContainer.GetAllInstances);
            var command = new TestCommand();

            bus.Send(command);

            command.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void When_sending_a_command_and_there_is_multiple_command_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleMessages<TestCommand>, TestCommandHandler>();            
            serviceContainer.Register<IHandleMessages<TestCommand>, AnotherTestCommandHandler>("another");
            var bus = new Bus(serviceContainer.GetAllInstances);
            var command = new TestCommand();

            Assert.Throws<NotSupportedException>(() => bus.Send(command));
        }

        [Fact]
        public void When_sending_a_command_and_there_are_no_command_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            var bus = new Bus(serviceContainer.GetAllInstances);
            var command = new TestCommand();

            Assert.Throws<NotSupportedException>(() => bus.Send(command));
        }

        [Fact]
        public void When_publishing_an_event_should_invoke_all_registered_event_handlers()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterAssembly(typeof (BusTests).Assembly, (serviceType, implementingType) => serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof (IHandleMessages<>));
            var bus = new Bus(serviceContainer.GetAllInstances);
            var message = new TestEvent();

            bus.Publish(message);

            message.NumberOfTimesHandled.ShouldEqual(2);
        }

        [Fact]
        public void When_sending_a_request_and_there_is_only_one_requeset_handler_should_invoke_request_handler()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequests<TestRequest, TestResponse>, TestRequestHandler>();
            var bus = new Bus(serviceContainer.GetAllInstances);
            var request = new TestRequest();

            var response = bus.Get(request);

            response.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void When_sending_a_request_and_there_are_multiple_request_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequests<TestRequest, TestResponse>, TestRequestHandler>();
            serviceContainer.Register<IHandleRequests<TestRequest, TestResponse>, AnotherTestRequestHandler>("another");
            var bus = new Bus(serviceContainer.GetAllInstances);
            var request = new TestRequest();

            Assert.Throws<NotSupportedException>(() => bus.Get(request));
        }

        [Fact]
        public void When_sending_a_request_and_there_are_no_request_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            var bus = new Bus(serviceContainer.GetAllInstances);
            var request = new TestRequest();

            Assert.Throws<NotSupportedException>(() => bus.Get(request));
        }

        [Fact]
        public void When_sending_the_same_event_multiple_times_should_get_handlers_from_cahce()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterAssembly(typeof(BusTests).Assembly, (serviceType, implementingType) => serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IHandleMessages<>));
            var bus = new Bus(serviceContainer.GetAllInstances);
            var message = new TestEvent();

            Assert.DoesNotThrow(() =>
                {
                    bus.Publish(message);
                    bus.Publish(message);
                    bus.Publish(message);
                });
        }
    }
}