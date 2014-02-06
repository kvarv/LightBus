namespace LightBus
{
    public interface ISendCommands
    {
        void Send(ICommand message);
    }
}