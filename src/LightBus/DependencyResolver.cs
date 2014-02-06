namespace LightBus
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class DependencyResolver
    {
        private readonly Func<Type, IEnumerable<object>> _getAllInstancesOfType;
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _cache = new ConcurrentDictionary<Type, IEnumerable<object>>();

        public DependencyResolver(Func<Type, IEnumerable<object>> getAllInstancesOfType)
        {
            _getAllInstancesOfType = getAllInstancesOfType;
        }

        public IEnumerable<object> GetAllMessageHandlers(Type messageType)
        {
            return _cache.GetOrAdd(messageType, _ =>
                {
                    var handlerType = typeof (IHandleMessages<>).MakeGenericType(messageType);
                    return _getAllInstancesOfType(handlerType);
                });
        }

        public IEnumerable<object> GetAllRequestHandlers(Type requestType, Type responseType)
        {
            return _cache.GetOrAdd(requestType, _ =>
                {
                    var handlerType = typeof(IHandleRequests<,>).MakeGenericType(requestType, responseType);                
                    return _getAllInstancesOfType(handlerType);
                });
        }
    }
}