using System.Threading.Tasks;

namespace LightBus
{
    public interface ISendQueries
    {
        Task<TResponse> SendAsync<TResponse>(IQuery<TResponse> query);
    }
}