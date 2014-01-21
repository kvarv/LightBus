namespace LightBus
{
    public interface IPublishEvents
	{
		void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
	}
}