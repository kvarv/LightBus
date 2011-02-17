using StructureMap;

namespace Cqrs
{
	public static class Route<TMessage> where TMessage : IMessage
	{
		public static void To<THandler>() where THandler : IHandle<TMessage>
		{
			ObjectFactory.Configure(x => x.For<IHandle<TMessage>>().Use<THandler>());
		}
	}
}