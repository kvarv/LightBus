#LightBus

**LightBus** is a lightweight in-process bus, which pretty much makes it an implementation of the mediator pattern. It is insipired by [NServiceBus](http://www.particular.net/).

Typical use case is server side behind a service interface like ASP.NET Web Api, ASP.NET MVC, Nancy, ServiceStack, WCF etc. 

Handlers are created every time a message is sent. Because of this, **LightBus** is not useful as an Event Aggregator.

##Define a command
```csharp
public class CreateCustomerCommand : ICommand
{
    public string Name { get; set; }
}
```

##Send a command
```csharp
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
```

##Handle a command
```csharp
public class CreateCustomerHandler : IHandleMessages<CreateCustomerCommand>
{
    public void Handle(CreateCustomerCommand command)
    {
        Console.WriteLine("Creating customer {0}.", command.Name);
    }
}
```

##Define an event
```csharp
public class CustomerCreatedEvent : IEvent
{
    public Guid CustomerId { get; set; }
}
```

##Publish an event
```csharp
public class CreateCustomerHandler : IHandleMessages<CreateCustomerCommand>
{
    private readonly IBus _bus;

    public CreateCustomerHandler(IBus bus)
    {
        _bus = bus;
    }

    public void Handle(CreateCustomerCommand command)
    {
        Console.WriteLine("Creating customer {0}.", command.Name);
        _bus.Publish(new CustomerCreatedEvent { CustomerId = Guid.NewGuid() });
    }
}
```

##Handle an event
```csharp
public class CustomerCreatedHandler : IHandleMessages<CustomerCreatedEvent>
{
    public void Handle(CustomerCreatedEvent command)
    {
        Console.WriteLine("Customer with id {0} created.", command.CustomerId);            
    }
}
```
