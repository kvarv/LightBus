using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LightBus
{
    public class DependencyResolver
    {
        private readonly ConcurrentDictionary<Type, Type> _cache = new ConcurrentDictionary<Type, Type>();
        private readonly Func<Type, IEnumerable<object>> _getAllInstancesOfType;

        public DependencyResolver(Func<Type, IEnumerable<object>> getAllInstancesOfType)
        {
            _getAllInstancesOfType = getAllInstancesOfType;
        }

        public IEnumerable<object> GetAllMessageHandlers(Type messageType)
        {
            Func<Type, Type> handlerTypeFactory = t => typeof (IHandleEventsAsync<>).MakeGenericType(t);
            return GetAllInstancesOfType(messageType, handlerTypeFactory);
        }

        public IEnumerable<object> GetAllQueryHandlers(Type queryType, Type responseType)
        {
            Func<Type, Type> handlerTypeFactory = t => typeof (IHandleRequestsAsync<,>).MakeGenericType(queryType, responseType);
            return GetAllInstancesOfType(queryType, handlerTypeFactory);
        }

        private IEnumerable<object> GetAllInstancesOfType(Type messageType, Func<Type, Type> handlerTypeFactory)
        {
            var handlerType = _cache.GetOrAdd(messageType, handlerTypeFactory);
            return _getAllInstancesOfType(handlerType);
        }
    }
}