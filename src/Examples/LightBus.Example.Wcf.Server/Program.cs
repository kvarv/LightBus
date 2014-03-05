using System;
using System.Linq;
using System.Reflection;
using LightBus.Example.Wcf.Contracts;
using LightInject;

namespace LightBus.Example.Wcf.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "WCF Server";

            var serviceContainer = new ServiceContainer();
            serviceContainer.RegisterAssembly(Assembly.GetExecutingAssembly(), (serviceType, implementingType) => serviceType.IsGenericType && (serviceType.GetGenericTypeDefinition() == typeof(IHandleMessages<>) || serviceType.GetGenericTypeDefinition() == typeof(IHandleQueries<,>)));
            serviceContainer.Register<ICustomerService, CustomerService>();
            serviceContainer.Register<IBus>(sf => new Bus(sf.GetAllInstances), new PerContainerLifetime());

            serviceContainer.EnableWcf();

            var serviceHostFactory = new LightInjectServiceHostFactory();
            var url = "http://localhost:8080/customerservice";
            var serviceHost = serviceHostFactory.CreateServiceHost<ICustomerService>(url);
            serviceHost.Open();

            Console.WriteLine("Listening on {0}", url);
            Console.WriteLine("Press 'Enter' to stop the service.");

            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
