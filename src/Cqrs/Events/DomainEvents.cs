using System;
using StructureMap;

namespace Cqrs.Events
{
	public static class DomainEvents
	{
		public static void Publish(Event @event)
		{
			ObjectFactory.GetInstance<IDispatchEvents>().Dispatch(@event);
		}

		public static void Subscribe<TEvent>(Action<TEvent> handle) where TEvent : Event
		{
			ObjectFactory.GetInstance<IDispatchEvents>().RegisterHandler(handle);
		}
	}
}