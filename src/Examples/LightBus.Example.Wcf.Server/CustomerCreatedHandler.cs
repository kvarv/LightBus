using System;
using LightBus.Example.Wcf.Contracts;

namespace LightBus.Example.Wcf.Server
{
    public class CustomerCreatedHandler : IHandleMessages<CustomerCreatedEvent>
    {
        public void Handle(CustomerCreatedEvent command)
        {
            Console.WriteLine("Customer with id {0} created.", command.CustomerId);            
        }
    }
}