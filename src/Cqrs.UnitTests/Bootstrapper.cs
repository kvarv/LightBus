using StructureMap;

namespace Cqrs.UnitTests
{
	public class Bootstrapper
	{
		public Bootstrapper()
		{
			ObjectFactory.Configure(x => x.Scan(scanner =>
			{
				scanner.TheCallingAssembly();
				scanner.ConnectImplementationsToTypesClosing(typeof(IHandle<>));
			}));
		}
	}
}