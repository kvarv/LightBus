namespace LightBus
{
	public interface IHandle<T>  where T : IMessage
	{
		void Handle(T command);
	}
}