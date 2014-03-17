using System.Threading.Tasks;

namespace LightBus
{
    public interface IPublishEvents
    {
        Task PublishAsync(IEvent @event);
    }
}