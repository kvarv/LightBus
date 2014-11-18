using System;
using System.Threading.Tasks;
using LightBus.Tests.LightInject;
using Should;
using Xunit;

namespace LightBus.Tests
{
    public class MediatorTests
    {
        [Fact]
        public void When_sending_a_command_and_there_is_only_one_command_handler_should_invoke_command_handler()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequestsAsync<Command, Unit>, CommandHandler>();
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var command = new Command();

            bus.SendAsync(command);

            command.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void When_sending_a_command_and_there_are_no_command_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var command = new Command();

            Assert.Throws<NotSupportedException>(() => bus.SendAsync(command).Wait());
        }

        [Fact]
        public void When_publishing_an_event_should_invoke_all_registered_event_handlers()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterAssembly(typeof (MediatorTests).Assembly, (serviceType, implementingType) => serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof (IHandleEventsAsync<>));
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var message = new Event();

            bus.PublishAsync(message);

            message.NumberOfTimesHandled.ShouldEqual(2);
        }

        [Fact]
        public void When_publishing_an_event_should_polymorphic_dispatch_to_all_handlers()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleEventsAsync<Event>, EventHandler>();
            serviceContainer.Register<IHandleEventsAsync<Event>, AnotherEventHandler>("Another");
            var messageHandler = new MessageHandler();
            serviceContainer.RegisterInstance<IHandleEventsAsync<IEvent>>(messageHandler);
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var message = new Event();

            bus.PublishAsync(message);

            message.NumberOfTimesHandled.ShouldEqual(2);
            messageHandler.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void When_publishing_an_event_in_a_command_handler_should_handle_event()
        {
            var serviceContainer = new ServiceContainer();
            var bus = new Mediator(serviceContainer.GetAllInstances);
            serviceContainer.Register<IMediator>(sf => bus);
            serviceContainer.Register<IHandleRequestsAsync<Command, Unit>, CommandHandlerThatSendsAnEvent>();
            serviceContainer.Register<IHandleEventsAsync<EventWithCommand>, EventWithCommandHandler>();
            var command = new Command();

            bus.SendAsync(command);

            command.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public async Task When_sending_a_command_should_invoke_command_handler_and_be_able_to_wait_for_async_result()
        {
            var serviceContainer = new ServiceContainer();
            var bus = new Mediator(serviceContainer.GetAllInstances);
            serviceContainer.Register<IMediator>(sf => bus);
            serviceContainer.Register<IHandleRequestsAsync<AsyncCommand, Unit>, AsyncCommandHandler>();
            var command = new AsyncCommand();

            await bus.SendAsync(command);

            command.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void When_sending_a_query_and_there_is_only_one_query_handler_should_invoke_query_handler()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequestsAsync<Query, Response>, QueryHandler>();
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var query = new Query();

            var response = bus.SendAsync(query);

            response.Result.IsHandled.ShouldBeTrue();
        }

        [Fact]
        public void When_sending_a_query_and_there_are_multiple_query_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequestsAsync<Query, Response>, QueryHandler>();
            serviceContainer.Register<IHandleRequestsAsync<Query, Response>, AnotherQueryHandler>("another");
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var query = new Query();

            Assert.Throws<NotSupportedException>(() => bus.SendAsync(query).Wait());
        }

        [Fact]
        public void When_sending_a_query_and_there_are_no_query_handlers_should_throw_exception()
        {
            var serviceContainer = new ServiceContainer();
            var bus = new Mediator(serviceContainer.GetAllInstances);
            var query = new Query();

            Assert.Throws<NotSupportedException>(() => bus.SendAsync(query).Wait());
        }

        [Fact]
        public void When_sending_the_same_event_multiple_times_should_get_handlers_from_cahce()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterAssembly(typeof (MediatorTests).Assembly, (serviceType, implementingType) => serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof (IHandleEventsAsync<>));
            IMediator mediator = new Mediator(serviceContainer.GetAllInstances);
            var message = new Event();

            Assert.DoesNotThrow(() =>
            {
                mediator.PublishAsync(message);
                mediator.PublishAsync(message);
                mediator.PublishAsync(message);
            });
        }

        [Fact]
        public void When_command_handler_throws_exception_should_propagate_exception()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequestsAsync<CommandWithException, Unit>, CommandHandlerThatThrowException>();
            var bus = new Mediator(serviceContainer.GetAllInstances);

            Assert.Throws<InvalidOperationException>(() => bus.SendAsync(new CommandWithException()).Wait());
        }

        [Fact]
        public void When_event_handler_throws_exception_should_propagate_exception()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleEventsAsync<EventWithException>, EventWithExceptionHandler>();
            var bus = new Mediator(serviceContainer.GetAllInstances);

            Assert.Throws<InvalidOperationException>(() => bus.PublishAsync(new EventWithException()).Wait());
        }

        [Fact]
        public void When_query_handler_throws_exception_should_propagate_exception()
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.Register<IHandleRequestsAsync<QueryWithExcepetion, Response>, QueryWithExceptionHandler>();
            var bus = new Mediator(serviceContainer.GetAllInstances);

            Assert.Throws<InvalidOperationException>(() => bus.SendAsync(new QueryWithExcepetion()).Wait());
        }
    }
}