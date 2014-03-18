using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightBus
{
    public class Bus : IBus
    {
        private readonly DependencyResolver _dependencyResolver;

        public Bus(Func<Type, IEnumerable<object>> getAllInstancesOfType)
        {
            _dependencyResolver = new DependencyResolver(getAllInstancesOfType);
        }

        public Task PublishAsync(IEvent message)
        {
            var handlers = _dependencyResolver.GetAllMessageHandlers(message.GetType());
            var results = handlers.Cast<dynamic>().Select(handler => handler.HandleAsync((dynamic) message));
            var tasks = results.Select(task => (Task) task).ToArray();
            return Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                var exceptions = completedTasks.Where(task => task.Exception != null).Select(task => task.Exception);
                if (exceptions.Any())
                    throw new AggregateException(exceptions);
                return completedTasks;
            });
        }

        public Task SendAsync(ICommand message)
        {
            var commandType = message.GetType();
            var handlers = _dependencyResolver.GetAllMessageHandlers(commandType).ToList();
            handlers.CheckIfThereAreAnyFor(commandType);
            handlers.CheckIfThereIsMoreThanOneFor(commandType);
            dynamic handler = handlers.Single();
            return handler.HandleAsync((dynamic) message);
        }

        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlers = _dependencyResolver.GetAllQueryHandlers(queryType, typeof (TResponse)).ToList();
            handlers.CheckIfThereAreAnyFor(queryType);
            handlers.CheckIfThereIsMoreThanOneFor(queryType);
            dynamic handler = handlers.Single();
            return handler.HandleAsync((dynamic) query);
        }
    }
}