using System.Threading.Tasks;

namespace LightBus
{
    public interface IPublishEvents
    {
        /// <summary>
        /// Publish an event as an asynchronous operation.
        /// </summary>
        /// <param name="message">The <see cref="IEvent"/> to publish.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task PublishAsync(IEvent message);
    }
}