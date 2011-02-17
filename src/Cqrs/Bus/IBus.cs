namespace Cqrs.Bus
{
	public interface IBus : IPublishEvents, ISendCommands
	{
	}
}