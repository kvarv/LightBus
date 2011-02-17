using System;
using Cqrs;
using OrderDomain;

namespace OrderService
{
	public class CreateOrderHandler : IHandle<CreateOrderCommand>
	{
		public void Handle(CreateOrderCommand command)
		{
			var order = OrderFactory.CreateOrder();
			//Persist order
			Console.WriteLine("Order created and persisted");
		}
	}
}