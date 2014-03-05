namespace LightBus
{
    public interface IHandleMessages<in TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}