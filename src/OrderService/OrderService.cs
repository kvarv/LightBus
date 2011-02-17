using System.ServiceModel;
using Cqrs;
using Cqrs.Commands;

namespace OrderService
{
	[ServiceContract]
	public interface IOrderService
	{
		[OperationContract]
		void CreateOrder(CreateOrderCommand command);
	}

	public class OrderService : IOrderService
	{
		private readonly IDispatchCommands _commandDispatcher;

		public OrderService(IDispatchCommands commandDispatcher)
		{
			_commandDispatcher = commandDispatcher;
		}

		public void CreateOrder(CreateOrderCommand command)
		{
			_commandDispatcher.Dispatch(command);
		}
	}
}