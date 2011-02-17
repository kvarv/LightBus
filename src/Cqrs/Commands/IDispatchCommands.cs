namespace Cqrs.Commands
{
	public interface IDispatchCommands
	{
		void Dispatch<TCommand>(TCommand command) where TCommand : Command;
	}
}