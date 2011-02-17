using System;
using Cqrs.Events;

namespace Cqrs
{
	public interface IPublishEvents
	{
		void Publish(Event @event);
		void Subscribe<TEvent>(Action<TEvent> handle) where TEvent : Event;
	}
}