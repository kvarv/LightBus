using Cqrs;
using OrderDomain.Events;

namespace OrderService.Denormailzers
{
	public class CreateOrderDenormalizer : IHandle<OrderCreatedEvent>
	{
		public void Handle(OrderCreatedEvent command)
		{

		}
	}
}