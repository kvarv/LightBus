namespace LightBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Bus : IBus
    {
        private readonly Func<Type, IEnumerable<object>> _getAllHandlerInstancesFunc;

        public Bus(Func<Type, IEnumerable<object>> getAllHandlerInstancesFunc)
        {
            _getAllHandlerInstancesFunc = getAllHandlerInstancesFunc;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : Event
        {
            var handlers = GetAllHandlers(typeof (TEvent));
            var typeSaveHandlers = handlers.Cast<IHandle<TEvent>>();
            foreach (var handler in typeSaveHandlers)
            {
                handler.Handle(@event);
            }
        }

        public void Send<TCommand>(TCommand command) where TCommand : Command
        {
            var commandType = typeof (TCommand);
            var handlers = GetAllHandlers(commandType).ToList();
            CheckIfThereAreAnyCommandHandlers(handlers, commandType);
            CheckIfThereIsMoreThanOneCommandHandler(handlers, commandType);
            var handler = handlers.Cast<IHandle<TCommand>>().Single();
            handler.Handle(command);
        }

        private static void CheckIfThereIsMoreThanOneCommandHandler(IEnumerable<object> handlers, Type commandType)
        {
            if (handlers.Count() > 1)
            {
                throw new NotSupportedException(string.Format("There are more than one handler registered for the command {0}. A command should only have one handler.", commandType.FullName));
            }
        }

        private static void CheckIfThereAreAnyCommandHandlers(IEnumerable<object> handlers, Type commandType)
        {
            if (!handlers.Any())
            {
                throw new NotSupportedException(string.Format("There is no handler registered for the command {0}.", commandType.FullName));
            }
        }

        private IEnumerable<object> GetAllHandlers(Type genericArgumentType)
        {
            var handlerType = typeof (IHandle<>).MakeGenericType(genericArgumentType);
            var handlers = _getAllHandlerInstancesFunc(handlerType);
            return handlers;
        }
    }
}