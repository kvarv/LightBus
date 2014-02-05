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

        public IEnumerable<object> GetAllMessageHandlers(Type genericArgumentType)
        {
            var genericHandlerType = typeof (IHandleMessages<>).MakeGenericType(genericArgumentType);
            var handlers = GetHandlers(genericHandlerType);
            return handlers;
        }

        public IEnumerable<object> GetAllRequestHandlers(Type requestType, Type responseType)
        {
            var genericHandlerType = typeof (IHandleRequests<,>).MakeGenericType(requestType, responseType);
            var handlers = GetHandlers(genericHandlerType);
            return handlers;
        }

        private IEnumerable<object> GetHandlers(Type type)
        {
            return _cache.GetOrAdd(type, _getAllInstancesOfType);
        }
    }
}