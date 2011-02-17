using System;
using StructureMap;

namespace Cqrs.Commands
{
	public class CommandDispatcher : IDispatchCommands
	{
		public void Dispatch<TCommand>(TCommand command) where TCommand : Command
		{
			var handler = ObjectFactory.TryGetInstance<IHandle<TCommand>>();
			if (handler == null)
				throw new InvalidOperationException(string.Format("You either have not specified a handler, or you have specified more than one handler for the command {0}.", typeof(TCommand).FullName));
			handler.Handle(command);
		}
	}
}