using System;
using System.Collections.Generic;
using System.Reflection;
using LightInject;

namespace LightBus.LightInject
{
    public class LightInjectConfigurator : IConfigurator
    {
        private readonly IServiceContainer _serviceContainer;

        public LightInjectConfigurator(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        public void RegisterHandlersFrom(params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                _serviceContainer.RegisterAssembly(assembly, (serviceType, implementingType) => serviceType.IsGenericType && (serviceType.GetGenericTypeDefinition() == typeof (IHandleMessages<>) || serviceType.GetGenericTypeDefinition() == typeof (IHandleRequests<,>)));
            }
        }

        public Func<Type, IEnumerable<object>> GetAllInstancesOfType
        {
            get { return _serviceContainer.GetAllInstances; }
        }
    }
}