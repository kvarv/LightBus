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

        public void Publish(IEvent message)
        {
            var handlers = _dependencyResolver.GetAllMessageHandlers(message.GetType());
            foreach (dynamic handler in handlers)
            {
                handler.Handle((dynamic) message);
            }
        }

        public void Send(ICommand message)
        {
            var commandType = message.GetType();
            var handlers = _dependencyResolver.GetAllMessageHandlers(commandType).ToList();
            handlers.CheckIfThereAreAnyFor(commandType);
            handlers.CheckIfThereIsMoreThanOneFor(commandType);
            dynamic handler = handlers.Single();
            handler.Handle((dynamic) message);
        }

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var requestType = request.GetType();
            var handlers = _dependencyResolver.GetAllRequestHandlers(requestType, typeof (TResponse)).ToList();
            handlers.CheckIfThereAreAnyFor(requestType);
            handlers.CheckIfThereIsMoreThanOneFor(requestType);
            dynamic handler = handlers.Single();
            return (TResponse) handler.Handle((dynamic) request);
        }
    }
}