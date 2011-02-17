using System;
using System.ServiceModel;
using OrderService;

namespace Client
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Client";

			var service = GetService<IOrderService>("http://localhost:8080/order");

			Console.WriteLine("Press 'Enter' to create an order. To exit, press 'q' and then 'Enter'.");

			while ((Console.ReadLine()) != "q")
			{
				var order = new CreateOrderCommand();
				service.CreateOrder(order);
			}
		}

		private static T GetService<T>(string endpointAddress)
		{
			var cf = new ChannelFactory<T>(new BasicHttpBinding(), new EndpointAddress(endpointAddress));
			return cf.CreateChannel();
		}
	}
}
