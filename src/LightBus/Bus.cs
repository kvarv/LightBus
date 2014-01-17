namespace LightBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using LightInject;

    public class Bus : IBus
    {
        private readonly ServiceContainer _container;

        private readonly List<Delegate> _eventCallbacks = new List<Delegate>();

        private Bus(ServiceContainer container)
        {
            _container = container;
        }

        public static Bus Create(params Assembly[] assemblies)
        {
            var serviceContainer = new ServiceContainer();
            ICompositionRoot compositionRoot = new CompositionRoot(assemblies);
            compositionRoot.Compose(serviceContainer);
            return new Bus(serviceContainer);
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var handlers = _container.GetAllInstances<IHandle<TEvent>>().ToList();
            handlers.ForEach(handler => handler.Handle(@event));
            _eventCallbacks.ForEach(callback =>
            {
                if (callback is Action<TEvent>)
                {
                    ((Action<TEvent>)callback)(@event);
                }
            });
        }

        public void Send<TCommand>(TCommand command) where TCommand : Command
        {
            var handler = _container.TryGetInstance<IHandle<TCommand>>();
            if (handler == null)
            {
                throw new InvalidOperationException(string.Format("You either have not specified a handler, or you have specified more than one handler for the command {0}.", typeof(TCommand).FullName));
            }
            handler.Handle(command);
        }
    }
}