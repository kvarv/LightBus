using Cqrs;
using Cqrs.Events;

namespace OrderDomain.Events
{
	public class OrderCreatedEvent : Event
	{
		public OrderCreatedEvent(Order order)
		{
			Order = order;
		}

		public Order Order { get; private set; }
	}
}