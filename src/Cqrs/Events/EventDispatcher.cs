using System;
using System.Collections.Generic;
using System.Linq;
using StructureMap;

namespace Cqrs.Events
{
	public class EventDispatcher : IDispatchEvents
	{
		private readonly List<Delegate> _callbacks = new List<Delegate>();

		public void RegisterHandler<TEvent>(Action<TEvent> handle) where TEvent : Event
		{
			_callbacks.Add(handle);
		}

		public void Dispatch<TEvent>(TEvent @event) where TEvent : Event
		{
			var handlers = ObjectFactory.GetAllInstances<IHandle<TEvent>>().ToList();
			handlers.ForEach(handler => handler.Handle(@event));
			_callbacks.ForEach(callback =>
			                   	{
			                   		if (callback is Action<TEvent>)
			                   			((Action<TEvent>) callback)(@event);
			                   	});
		}
	}
}