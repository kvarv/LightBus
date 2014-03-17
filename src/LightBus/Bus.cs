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
            var whenAll = Task.Factory.ContinueWhenAll(tasks, completedTasks => completedTasks);
            //Since all async exceptions must be observed in 4.0
            return whenAll.ContinueWith(tsks => tsks.Result.Select(x => x.Exception));
        }

        public Task SendAsync(ICommand message)
        {
            var commandType = message.GetType();
            var handlers = _dependencyResolver.GetAllMessageHandlers(commandType).ToList();
            handlers.CheckIfThereAreAnyFor(commandType);
            handlers.CheckIfThereIsMoreThanOneFor(commandType);
            dynamic handler = handlers.Single();
            Task task = handler.HandleAsync((dynamic) message);
            //Since all async exceptions must be observed in 4.0
            return task.ContinueWith(tsk => tsk.Exception);
        }

        public Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query)
        {
            var queryType = query.GetType();
            var handlers = _dependencyResolver.GetAllQueryHandlers(queryType, typeof (TResponse)).ToList();
            handlers.CheckIfThereAreAnyFor(queryType);
            handlers.CheckIfThereIsMoreThanOneFor(queryType);
            dynamic handler = handlers.Single();
            Task<TResponse> task = handler.HandleAsync((dynamic) query);
            //Since all async operations must be observed in 4.0
            return task.ContinueWith(tsk => tsk.Result);
        }
    }
}