using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightBus
{
    /// <summary>
    /// A class that implements <see cref="IMediator"/> that can be used to send a request or publish an event.
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

        public TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            var queryType = request.GetType();
            var handlers = _dependencyResolver.GetAllRequestHandlers(queryType, typeof(TResponse)).ToList();
            handlers.CheckIfThereAreAnyFor(queryType);
            handlers.CheckIfThereIsMoreThanOneFor(queryType);
            dynamic handler = handlers.Single();
            return handler.Handle((dynamic)request);
        }

        public void Publish(IEvent @event)
        {
            var messageType = @event.GetType();
            var interfaceTypes = messageType.GetInterfaces();
            var types = new List<Type> { messageType };
            types.AddRange(interfaceTypes);
            var handlers = types.SelectMany(x => _dependencyResolver.GetAllEventHandlers(x));

            foreach (dynamic handler in handlers)
            {
                handler.Handle((dynamic)@event);
            }
        }

        /// <summary>
        /// Send a request as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResponse">The response of the request.</typeparam>
        /// <param name="request">The <see cref="IRequest{TResponse}"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>   
        public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            var queryType = request.GetType();
            var handlers = _dependencyResolver.GetAllAsyncRequestHandlers(queryType, typeof(TResponse)).ToList();
            handlers.CheckIfThereAreAnyFor(queryType);
            handlers.CheckIfThereIsMoreThanOneFor(queryType);
            dynamic handler = handlers.Single();
            return handler.Handle((dynamic)request);
        }

        /// <summary>
        /// Publish an event as an asynchronous operation.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> to publish.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task PublishAsync(IEvent @event)
        {
            var messageType = @event.GetType();
            var interfaceTypes = messageType.GetInterfaces();
            var types = new List<Type> { messageType };
            types.AddRange(interfaceTypes);
            var handlers = types.SelectMany(x => _dependencyResolver.GetAllAsyncEventHandlers(x));
            var results = handlers.Cast<dynamic>().Select(handler => handler.Handle((dynamic)@event));
            var tasks = results.Select(task => (Task)task).ToArray();
            return Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                var exceptions = completedTasks.Where(task => task.Exception != null).Select(task => task.Exception).ToList();
                if (exceptions.Any())
                    throw new AggregateException(exceptions);
                return completedTasks;
            });
        }
    }
}