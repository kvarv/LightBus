using System.Threading.Tasks;

namespace LightBus
{
    public interface ISendQueries
    {
        /// <summary>
        /// Send a query as an asynchronous operation.
        /// </summary>
        /// <typeparam name="TResponse">The response of the query.</typeparam>
        /// <param name="query">The <see cref="IQuery{TResponse}"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>        
        Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);
    }
}