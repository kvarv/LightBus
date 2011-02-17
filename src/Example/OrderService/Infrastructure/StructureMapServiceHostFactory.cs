using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Cqrs;
using StructureMap;

namespace OrderService
{
	public class StructureMapServiceHostFactory : ServiceHostFactory
	{
		public StructureMapServiceHostFactory()
		{
			ObjectFactory.Configure(x =>
			                        	{
			                        		x.AddRegistry<CqrsRegistry>();
			                        		x.Scan(scanner =>
			                        		       	{
			                        		       		scanner.TheCallingAssembly();
			                        		       		scanner.WithDefaultConventions();
			                        		       	});
			                        	});
		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			return new StructureMapServiceHost(serviceType, baseAddresses);
		}
	}
}