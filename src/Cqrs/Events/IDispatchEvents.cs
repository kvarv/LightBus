using System;

namespace Cqrs.Events
{
	public interface IDispatchEvents
	{
		void RegisterHandler<TEvent>(Action<TEvent> handle) where TEvent : Event;
		void Dispatch<TEvent>(TEvent @event) where TEvent : Event;
	}
}