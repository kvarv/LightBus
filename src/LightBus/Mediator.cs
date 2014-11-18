using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightBus
{
    /// <summary>
    /// A class that implements <see cref="IMediator"/> that can be used to asynchronously send or publish commands, events and queries.
    /// </summary>
    public class Mediator : IMediator
    {
        private readonly DependencyResolver _dependencyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/>.
        /// </summary>
        /// <param name="getAllInstancesOfType">The factory delegate used to resolve handlers.</param>
        public Mediator(Func<Type, IEnumerable<object>> getAllInstancesOfType)
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
            var messageType = message.GetType();
            var interfaceTypes = messageType.GetInterfaces();
            var types = new List<Type> { messageType };
            types.AddRange(interfaceTypes);
            var handlers = types.SelectMany(x => _dependencyResolver.GetAllMessageHandlers(x));
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
        /// Send a query as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResponse">The response of the query.</typeparam>
        /// <param name="query">The <see cref="IRequest{TResponse}"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>   
        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> query)
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