using Cqrs.Commands;
using Cqrs.Events;
using StructureMap.Configuration.DSL;

namespace Cqrs
{
	public class CqrsRegistry : Registry
	{
		public CqrsRegistry()
		{
			For<IDispatchCommands>().Singleton().Use<CommandDispatcher>();
			For<IDispatchEvents>().Singleton().Use<EventDispatcher>();
			Scan(scanner =>
			     	{
			     		scanner.TheCallingAssembly();
			     		scanner.WithDefaultConventions();
			     	});
		}
	}
}