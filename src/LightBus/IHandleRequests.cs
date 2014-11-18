using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleRequestsAsync<in TQuery, TResponse> where TQuery : IRequest<TResponse>
    {
        Task<TResponse> Handle(TQuery query);
    }

    public interface IHandleRequests<in TQuery, out TResponse> where TQuery : IRequest<TResponse>
    {
        TResponse Handle(TQuery query);
    }
}