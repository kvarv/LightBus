namespace LightBus
{
    using System;
    using System.Collections.Generic;

    public class DependencyResolver
    {
        private readonly Func<Type, IEnumerable<object>> _getAllInstancesOfType;

        public DependencyResolver(Func<Type, IEnumerable<object>> getAllInstancesOfType)
        {
            _getAllInstancesOfType = getAllInstancesOfType;
        }

        public IEnumerable<object> GetAllMessageHandlers(Type genericArgumentType)
        {
            var genericHandlerType = typeof(IHandleMessages<>).MakeGenericType(genericArgumentType);
            var handlers = _getAllInstancesOfType(genericHandlerType);
            return handlers;
        }

        public IEnumerable<object> GetAllRequestHandlers(Type requestType, Type responseType)
        {
            var genericHandlerType = typeof(IHandleRequests<,>).MakeGenericType(requestType, responseType);
            var handlers = _getAllInstancesOfType(genericHandlerType);
            return handlers;
        }
    }
}