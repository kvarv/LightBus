namespace LightBus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Bus : IBus
    {
        private readonly Func<Type, IEnumerable<object>> _getAllHandlersForMessageTypeFunc;

        public Bus(Func<Type, IEnumerable<object>> getAllHandlersForMessageTypeFunc)
        {
            _getAllHandlersForMessageTypeFunc = getAllHandlersForMessageTypeFunc;
        }

        public Bus(IConfigurator configurator)
        {
            _getAllHandlersForMessageTypeFunc = configurator.GetAllHandlersForMessageType;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var handlers = GetAllMessageHandlers(typeof (TEvent));
            var typeSaveHandlers = handlers.Cast<IHandleMessages<TEvent>>();
            foreach (var handler in typeSaveHandlers)
            {
                handler.Handle(@event);
            }
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandType = typeof (TCommand);
            var handlers = GetAllMessageHandlers(commandType).ToList();
            CheckIfThereAreAnyHandlers(handlers, commandType);
            CheckIfThereIsMoreThanOneHandler(handlers, commandType);
            var handler = (IHandleMessages<TCommand>) handlers.Single();
            handler.Handle(command);
        }

        public TResponse Get<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var handlers = GetAllRequestHandlers(requestType, typeof(TResponse)).ToList();
            CheckIfThereAreAnyHandlers(handlers, requestType);
            CheckIfThereIsMoreThanOneHandler(handlers, requestType);
            var handler = handlers.Single();
            return (TResponse) handler.GetType().GetMethod("Handle").Invoke(handler, new object[] {request});
        }

        private static void CheckIfThereIsMoreThanOneHandler(IEnumerable<object> handlers, Type messageType)
        {
            if (handlers.Count() > 1)
            {
                throw new NotSupportedException(string.Format("There are more than one handler registered for {0}. A command should only have one handler.", messageType.FullName));
            }
        }

        private static void CheckIfThereAreAnyHandlers(IEnumerable<object> handlers, Type messageType)
        {
            if (!handlers.Any())
            {
                throw new NotSupportedException(string.Format("There is no handler registered for the {0}.", messageType.FullName));
            }
        }

        private IEnumerable<object> GetAllMessageHandlers(Type genericArgumentType)
        {
            var genericHandlerType = typeof (IHandleMessages<>).MakeGenericType(genericArgumentType);
            var handlers = _getAllHandlersForMessageTypeFunc(genericHandlerType);
            return handlers;
        }

        private IEnumerable<object> GetAllRequestHandlers(Type requestType, Type responseType)
        {
            var genericHandlerType = typeof(IHandleRequests<,>).MakeGenericType(requestType, responseType);
            var handlers = _getAllHandlersForMessageTypeFunc(genericHandlerType);
            return handlers;
        }
    }
}