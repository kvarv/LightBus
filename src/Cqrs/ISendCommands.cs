using Cqrs.Commands;

namespace Cqrs
{
	public interface ISendCommands
	{
		void Send(Command command);
	}
}