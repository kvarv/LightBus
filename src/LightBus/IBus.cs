namespace LightBus
{
    /// <summary>
    /// Defines a set of methods used to asynchronously send or publish commands, events and queries.
    /// </summary>
    public interface IBus : IPublishEvents, ISendCommands, ISendQueries
    {
    }
}