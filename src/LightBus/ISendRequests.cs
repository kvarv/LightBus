namespace LightBus
{
    public interface ISendRequests
    {
        TResponse Send<TResponse>(IRequest<TResponse> request);
    }
}