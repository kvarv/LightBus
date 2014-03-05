using System;

namespace LightBus.Example.Wcf.Contracts
{
    public class CustomerCreatedEvent : IEvent
    {
        public Guid CustomerId { get; set; }
    }
}