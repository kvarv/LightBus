#LightBus

**LightBus** is a lightweight in-process bus, which pretty much makes it an implementation of the mediator pattern.

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
