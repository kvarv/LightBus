using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleEventsAsync<in TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }

    public interface IHandleEvents<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}