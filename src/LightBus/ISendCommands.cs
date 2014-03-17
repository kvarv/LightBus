using System.Threading.Tasks;

namespace LightBus
{
    public interface ISendCommands
    {
        Task SendAsync(ICommand message);
    }
}