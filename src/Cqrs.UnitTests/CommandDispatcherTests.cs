using Cqrs.Commands;
using Xunit;

namespace Cqrs.UnitTests
{
	public class CommandDispatcherTests
	{
		[Fact]
		public void Should_route_command_to_correct_handler()
		{
			var commandDispatcher = new CommandDispatcher();
			Route<TestCommand>.To<TestCommandHandler>();
			var testMessage = new TestCommand();

			commandDispatcher.Dispatch(testMessage);
			
			testMessage.IsHandled.ShouldBeTrue();
		}
	}
}