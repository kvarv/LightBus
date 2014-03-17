using System.Threading.Tasks;

namespace LightBus
{
    public interface IHandleMessages<in TMessage> where TMessage : IMessage
    {
        Task HandleAsync(TMessage message);
    }
}