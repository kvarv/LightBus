namespace LightBus
{
    public interface ISendCommands
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}