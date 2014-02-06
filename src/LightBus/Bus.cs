using System;
using System.Collections.Generic;
using System.Linq;

namespace LightBus
{
    public class Bus : IBus
    {
        private readonly DependencyResolver _dependencyResolver;

        public Bus(Func<Type, IEnumerable<object>> getAllInstancesOfType)
        {
            _dependencyResolver = new DependencyResolver(getAllInstancesOfType);
        }

        public Bus(IConfigurator configurator)
        {
            _dependencyResolver = new DependencyResolver(configurator.GetAllInstancesOfType);
        }

        public void Publish(IEvent message)
        {
            var handlers = _dependencyResolver.GetAllMessageHandlers(message.GetType());
            foreach (var handler in handlers)
            {
                handler.GetType().GetMethod("Handle").Invoke(handler, new object[] { message });
            }
        }

        public void Send(ICommand message)
        {
            var commandType = message.GetType();
            var handlers = _dependencyResolver.GetAllMessageHandlers(commandType).ToList();
            CheckIfThereAreAnyHandlers(handlers, commandType);
            CheckIfThereIsMoreThanOneHandler(handlers, commandType);
            var handler = handlers.Single();
            handler.GetType().GetMethod("Handle").Invoke(handler, new object[] { message });
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var handlers = _dependencyResolver.GetAllRequestHandlers(requestType, typeof (TResponse)).ToList();
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
    }
}