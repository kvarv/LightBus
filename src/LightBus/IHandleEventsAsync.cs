using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleEventsAsync<in TMessage> where TMessage : IEvent
    {
        Task HandleAsync(TMessage message);
    }
}