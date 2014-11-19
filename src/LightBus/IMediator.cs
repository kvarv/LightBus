using System.Threading.Tasks;

namespace LightBus
{
    /// <summary>
    /// Defines a set of methods used to send requests and publish events.
    /// </summary>
    public interface IMediator
    {
        /// <summary>
        /// Send a request (command or query) as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResponse">The response of the request.</typeparam>
        /// <param name="request">The <see cref="IRequest{TResponse}"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>   
        Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request);

        /// <summary>
        /// Publish an event as an asynchronous operation.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> to publish.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task PublishAsync(IEvent @event);

        /// <summary>
        /// Send a request (command or query).
        /// </summary>
        /// <typeparam name="TResponse">The response of the request.</typeparam>
        /// <param name="request">The <see cref="IRequest{TResponse}"/> to send.</param>
        /// <returns>The response.</returns>  
        TResponse Send<TResponse>(IRequest<TResponse> request);

        /// <summary>
        /// Publish an event.
        /// </summary>
        /// <param name="event">The <see cref="IEvent"/> to publish.</param>
        void Publish(IEvent @event);
    }
}