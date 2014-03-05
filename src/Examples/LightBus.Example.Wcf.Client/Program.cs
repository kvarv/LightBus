using System;
using System.ServiceModel;
using LightBus.Example.Wcf.Contracts;

namespace LightBus.Example.Wcf.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "WCF Client";

            var service = GetService<ICustomerService>("http://localhost:8080/customerservice");

            Console.WriteLine("Type a name and press 'Enter' to create a customer. To exit, press 'q' and then 'Enter'.");

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "q")
                    break;
                var order = new CreateCustomerCommand {Name = input};
                service.Send(order);
            }
        }

        private static T GetService<T>(string endpointAddress)
        {
            var cf = new ChannelFactory<T>(new BasicHttpBinding(), new EndpointAddress(endpointAddress));
            return cf.CreateChannel();
        }
    }
}