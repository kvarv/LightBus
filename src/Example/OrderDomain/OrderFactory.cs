using Cqrs;
using Cqrs.Events;
using OrderDomain.Events;

namespace OrderDomain
{
	public static class OrderFactory
	{
		public static Order CreateOrder()
		{
			var order = new Order();
			DomainEvents.Publish(new OrderCreatedEvent(order));
			return order;
		}
	}
}