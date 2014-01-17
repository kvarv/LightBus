namespace LightBus
{
    using System.Reflection;

    using LightBus.LightInject;

    public class CompositionRoot : ICompositionRoot
    {
        private readonly Assembly[] _assemblies;

        public CompositionRoot(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        void ICompositionRoot.Compose(IServiceRegistry serviceRegistry)
        {
            foreach (var assembly in _assemblies)
            {
                serviceRegistry.RegisterAssembly(assembly, (serviceType, implementingType) => serviceType.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IHandle<>));                
            }
        }
    }
}