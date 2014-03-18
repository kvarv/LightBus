using System.Threading.Tasks;

namespace LightBus
{
    public interface ISendCommands
    {
        /// <summary>
        /// Send a command as an asynchronous operation.
        /// </summary>
        /// <param name="message">The <see cref="ICommand"/> to send.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task SendAsync(ICommand message);
    }
}