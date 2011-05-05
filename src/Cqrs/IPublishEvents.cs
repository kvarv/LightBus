using System;
using Cqrs.Events;

namespace Cqrs
{
	public interface IPublishEvents
	{
		void Publish<TEvent>(TEvent @event) where TEvent : Event;
		void Subscribe<TEvent>(Action<TEvent> handle) where TEvent : Event;
	}
}