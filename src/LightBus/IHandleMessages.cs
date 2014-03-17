using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleMessages<in TMessage> where TMessage : IMessage
    {
        System.Threading.Tasks.Task HandleAsync(TMessage message);
    }
}