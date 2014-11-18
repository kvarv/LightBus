using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleRequestsAsync<in TQuery, TResponse> where TQuery : IRequest<TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}