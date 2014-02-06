namespace LightBus
{
    public interface IPublishEvents
    {
        void Publish(IEvent @event);
    }
}