using System.ServiceModel;

namespace LightBus.Example.Wcf.Contracts
{
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        void Send(CreateCustomerCommand command);
    }
}