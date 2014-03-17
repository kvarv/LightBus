using System.Threading.Tasks;

namespace LightBus
{
    public interface IPublishEvents
    {
        System.Threading.Tasks.Task PublishAsync(IEvent @event);
    }
}