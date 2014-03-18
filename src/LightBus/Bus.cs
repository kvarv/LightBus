using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightBus
{
    /// <summary>
    /// A class that implements <see cref="IBus"/> that can be used to asynchronously send or publish commands, events and queries.
    /// </summary>
    public class Bus : IBus
    {
        private readonly DependencyResolver _dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bus"/>.
        /// </summary>
        /// <param name="getAllInstancesOfType">The factory delegate used to resolve handlers.</param>
        public Bus(Func<Type, IEnumerable<object>> getAllInstancesOfType)
        {
            _dependencyResolver = new DependencyResolver(getAllInstancesOfType);
        }

        /// <summary>
        /// Publish an event as an asynchronous operation.
        /// </summary>
        /// <param name="message">The <see cref="IEvent"/> to publish.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task PublishAsync(IEvent message)
        {
            var handlers = _dependencyResolver.GetAllMessageHandlers(message.GetType());
            var results = handlers.Cast<dynamic>().Select(handler => handler.HandleAsync((dynamic)message));
            var tasks = results.Select(task => (Task)task).ToArray();
            return Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                var exceptions = completedTasks.Where(task => task.Exception != null).Select(task => task.Exception);
                if (exceptions.Any())
                    throw new AggregateException(exceptions);
                return completedTasks;
            });
        }

        /// <summary>
        /// Send a command as an asynchronous operation.
        /// </summary>
        /// <param name="message">The <see cref="ICommand"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SendAsync(ICommand message)
        {
            var commandType = message.GetType();
            var handlers = _dependencyResolver.GetAllMessageHandlers(commandType).ToList();
            handlers.CheckIfThereAreAnyFor(commandType);
            handlers.CheckIfThereIsMoreThanOneFor(commandType);
            dynamic handler = handlers.Single();
            return handler.HandleAsync((dynamic)message);
        }

        /// <summary>
        /// Send a query as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResponse">The response of the query.</typeparam>
        /// <param name="query">The <see cref="IQuery{TResponse}"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>   
        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlers = _dependencyResolver.GetAllQueryHandlers(queryType, typeof(TResponse)).ToList();
            handlers.CheckIfThereAreAnyFor(queryType);
            handlers.CheckIfThereIsMoreThanOneFor(queryType);
            dynamic handler = handlers.Single();
            return handler.HandleAsync((dynamic)query);
        }
    }
}