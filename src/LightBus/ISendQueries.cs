namespace LightBus
{
    public interface ISendQueries
    {
        TResponse Send<TResponse>(IQuery<TResponse> query);
    }
}