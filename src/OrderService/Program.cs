using System;
using Cqrs;
using StructureMap;

namespace OrderService
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ObjectFactory.Configure(x =>
			                        	{
			                        		x.AddRegistry<CqrsRegistry>();
			                        		x.Scan(scanner =>
			                        		       	{
			                        		       		scanner.TheCallingAssembly();
			                        		       		scanner.WithDefaultConventions();
			                        		       		scanner.ConnectImplementationsToTypesClosing(typeof (IHandle<>));
			                        		       	});
			                        	});

			var host = new StructureMapServiceHost(typeof (OrderService), new Uri("http://localhost:8080/order"));
			
			host.Open();

			foreach (var se in host.Description.Endpoints)
			{
				Console.WriteLine("{0}, {1}, {2}", se.Contract.Name, se.Address, se.Binding.Name);
			}

			Console.WriteLine("Press <Enter> to stop the service.");

			Console.ReadLine();

			host.Close();
		}
	}
}