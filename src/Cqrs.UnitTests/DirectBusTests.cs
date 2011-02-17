using System;
using Cqrs.Bus;
using Cqrs.Commands;
using Cqrs.Events;
using Rhino.Mocks;
using Xunit;

namespace Cqrs.UnitTests
{
	public class DirectBusTests
	{
		[Fact]
		public void Should_dispatch_command_via_command_dispatcher()
		{
			var commandDispatcher = MockRepository.GenerateMock<IDispatchCommands>();
			ISendCommands bus = new DirectBus(null, commandDispatcher);
			Command message = new TestCommand();
			bus.Send(message);
			commandDispatcher.AssertWasCalled(me => me.Dispatch(message));
		}

		[Fact]
		public void Should_dispatch_event_via_event_dispatcher()
		{
			var eventDispatcher = MockRepository.GenerateMock<IDispatchEvents>();
			IPublishEvents bus = new DirectBus(eventDispatcher, null);
			Event message = new TestEvent();
			bus.Publish(message);
			eventDispatcher.AssertWasCalled(me => me.Dispatch(message));
		}

		[Fact]
		public void Should_register_event_handler_in_event_dispathcer()
		{
			var eventDispatcher = MockRepository.GenerateMock<IDispatchEvents>();
			IPublishEvents bus = new DirectBus(eventDispatcher, null);
			Action<TestEvent> action = x => { };
			bus.Subscribe(action);
			eventDispatcher.AssertWasCalled(me => me.RegisterHandler(action));
		}
	}
}