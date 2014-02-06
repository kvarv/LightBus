using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LightBus
{
    public class DependencyResolver
    {
        private readonly ConcurrentDictionary<Type, IEnumerable<object>> _cache = new ConcurrentDictionary<Type, IEnumerable<object>>();
        private readonly Func<Type, IEnumerable<object>> _getAllInstancesOfType;

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
                    var handlerType = typeof (IHandleRequests<,>).MakeGenericType(requestType, responseType);
                    return _getAllInstancesOfType(handlerType);
                });
        }
    }
}