using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using StructureMap;

namespace OrderService
{
	public class StructureMapInstanceProvider : IInstanceProvider
	{
		private readonly Type _serviceType;

		public StructureMapInstanceProvider(Type serviceType)
		{
			_serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return ObjectFactory.GetInstance(_serviceType);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
		}
	}
}