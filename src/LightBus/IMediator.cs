using System.Threading.Tasks;

namespace LightBus
{
    /// <summary>
    /// Defines a set of methods used to asynchronously send or publish commands, events and queries.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Send a request (command or query) as an asynchronous operation.
        /// </summary>
        /// <param name="message">The <see cref="ICommand"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> message);

        /// <summary>
        /// Publish an event as an asynchronous operation.
        /// </summary>
        /// <param name="message">The <see cref="IEvent"/> to publish.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task PublishAsync(IEvent message);
    }
}