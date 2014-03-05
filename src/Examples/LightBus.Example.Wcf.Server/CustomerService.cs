using LightBus.Example.Wcf.Contracts;

namespace LightBus.Example.Wcf.Server
{
    public class CustomerService : ICustomerService
    {
        private readonly IBus _bus;

        public CustomerService(IBus bus)
        {
            _bus = bus;
        }

        public void Send(CreateCustomerCommand command)
        {
            _bus.Send(command);
        }
    }
}