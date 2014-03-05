using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using LightInject;
using LightInject.Interception;

namespace LightBus.Example.Wcf.Server
{
    public class LightInjectServiceHostFactory : ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var attribute = serviceType.GetCustomAttributes(typeof (ServiceContractAttribute), true)
                                       .Cast<ServiceContractAttribute>()
                                       .FirstOrDefault();

            if (!serviceType.IsInterface || attribute == null)
                throw new NotSupportedException("Only interfaces with [ServiceContract] attribute are supported with LightInjectServiceHostFactory.");

            if (LightInject.ServiceContainer == null)
                throw new ArgumentNullException("Wcf is not enabled on the ServiceContainer.");

            var container = LightInject.ServiceContainer;
            var proxyBuilder = new ProxyBuilder();
            var proxyDefinition = new ProxyDefinition(serviceType, () => container.GetInstance(serviceType));
            var proxyType = proxyBuilder.GetProxyType(proxyDefinition);

            return base.CreateServiceHost(proxyType, baseAddresses);
        }

        public ServiceHost CreateServiceHost<TService>(params string[] baseAddresses)
        {
            var uriBaseAddresses = baseAddresses.Select(s => new Uri(s)).ToArray();
            return CreateServiceHost(typeof (TService), uriBaseAddresses);
        }

        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            var type = Type.GetType(constructorString);
            if (type == null)
                throw new ArgumentException("Could not get Type for {0}", constructorString);

            return CreateServiceHost(type, baseAddresses);
        }
    }

    public static class LightInject
    {
        public static ServiceContainer ServiceContainer { get; set; }
    }

    public static class ServiceContainerExtension
    {
        public static void EnableWcf(this ServiceContainer serviceContainer)
        {
            LightInject.ServiceContainer = serviceContainer;
        }
    }
}