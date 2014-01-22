namespace LightBus
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IConfigurator
    {
        void RegisterHandlersFrom(params Assembly[] assemblies);
        Func<Type, IEnumerable<object>> GetAllHandlersForMessageType { get; }
    }
}