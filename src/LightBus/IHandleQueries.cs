using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleQueries<in TQuery, TResponse> where TQuery : IQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}