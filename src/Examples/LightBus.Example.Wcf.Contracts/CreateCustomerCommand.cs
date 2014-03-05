namespace LightBus.Example.Wcf.Contracts
{
    public class CreateCustomerCommand : ICommand
    {
        public string Name { get; set; }
    }
}