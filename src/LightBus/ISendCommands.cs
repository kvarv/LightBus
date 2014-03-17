using System.Threading.Tasks;

namespace LightBus
{
    public interface ISendCommands
    {
        System.Threading.Tasks.Task SendAsync(ICommand message);
    }
}