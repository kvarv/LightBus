using System;
using System.Collections.Generic;
using System.Reflection;

namespace LightBus
{
    public interface IConfigurator
    {
        Func<Type, IEnumerable<object>> GetAllInstancesOfType { get; }
        void RegisterHandlersFrom(params Assembly[] assemblies);
    }
}