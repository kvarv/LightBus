using Cqrs.Commands;

namespace Cqrs
{
	public interface ISendCommands
	{
		void Send<TCommand>(TCommand command) where TCommand : Command;
	}
}