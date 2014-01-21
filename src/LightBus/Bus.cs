namespace LightBus
{
    using System;
    using System.Linq;
    using System.Reflection;

    using LightInject;

    public class Bus : IBus
    {
        private readonly ServiceContainer _container;

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
            var handlerType = typeof(IHandle<>).MakeGenericType(typeof(TEvent));
            var handlers = _container.GetAllInstances(handlerType);
            var typeSaveHandlers = handlers.Cast<IHandle<TEvent>>();
            foreach (var handler in typeSaveHandlers)
            {
                handler.Handle(@event);
            }
        }

        public void Send<TCommand>(TCommand command) where TCommand : Command
        {
            var handlerType = typeof(IHandle<>).MakeGenericType(typeof(TCommand));
            var handlers = _container.GetAllInstances(handlerType);
            if (handlers.Count() != 1)
            {
                throw new InvalidOperationException(string.Format("You either have not specified a handler, or you have specified more than one handler for the command {0}.", typeof(TCommand).FullName));                
            }
            var handler = handlers.Cast<IHandle<TCommand>>().Single();
            handler.Handle(command);
        }
    }
}