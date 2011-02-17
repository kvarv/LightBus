using System;
using Cqrs.Commands;
using Cqrs.Events;

namespace Cqrs.Bus
{
	public class DirectBus : IBus
	{
		private readonly IDispatchEvents _eventDispatcher;
		private readonly IDispatchCommands _commandDispatcher;

		public DirectBus(IDispatchEvents eventDispatcher, IDispatchCommands commandDispatcher)
		{
			_eventDispatcher = eventDispatcher;
			_commandDispatcher = commandDispatcher;
		}

		public void Publish(Event @event)
		{
			_eventDispatcher.Dispatch(@event);
		}

		public void Subscribe<TEvent>(Action<TEvent> handle) where TEvent : Event
		{
			_eventDispatcher.RegisterHandler(handle);
		}

		public void Send(Command command)
		{
			_commandDispatcher.Dispatch(command);
		}
	}
}