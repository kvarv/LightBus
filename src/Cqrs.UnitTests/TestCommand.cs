using Cqrs.Commands;

namespace Cqrs.UnitTests
{
	public class TestCommand : Command
	{
		public bool IsHandled { get;  set; }
	}
}