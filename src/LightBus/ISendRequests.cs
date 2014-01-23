namespace LightBus
{
    public interface ISendRequests
    {
        TResponse Get<TResponse>(IRequest<TResponse> request);
    }
}