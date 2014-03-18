#LightBus

**LightBus** is a lightweight in-process bus, which pretty much makes it an implementation of the mediator pattern. **LightBus**, insipired by [NServiceBus](http://www.particular.net/) and the CQRS pattern, allows you to send messages (commands, events, queries) to registered recipients in a decoupled manner. Recipients, i.e. message handlers, are defined by marker interfaces and lets you encapsulate business operations in a single class, which in turn is registered and instantiated by an IoC container. 

Typical use case is directly behind your service boundary, for example server side behind a service interface like ASP.NET Web Api, ASP.NET MVC, Nancy, ServiceStack, WCF etc. 

**LightBus** is all async and uses the Task Parallell Library which means that you could use async/await on .NET 4.5. Still, **LightBus** targets .NET 4.0

##Installation
```PM> Install-Package LightBus```

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

    public async Task SendCommand(CreateCustomerCommand command)
    {
        await _bus.SendAsync(command);
    }
}
```

##Handle a command
```csharp
public class CreateCustomerHandler : IHandleMessages<CreateCustomerCommand>
{
    public async Task HandleAsync(CreateCustomerCommand command)
    {
        await Task.Delay(100);
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

    public async Task HandleAsync(CreateCustomerCommand command)
    {
        Console.WriteLine("Creating customer {0}.", command.Name);
        await _bus.PublishAsync(new CustomerCreatedEvent { CustomerId = Guid.NewGuid() });
    }
}
```

##Handle an event
```csharp
public class CustomerCreatedHandler : IHandleMessages<CustomerCreatedEvent>
{
    public async Task HandleAsync(CustomerCreatedEvent command)
    {
        await Task.Delay(50);
        Console.WriteLine("Customer with id {0} created.", command.CustomerId);
    }
}
```

##Define a query
```csharp
public class GetAllCustomersQuery : IQuery<GetAllCustomersResponse>
{
}
```

##Define a query response
```csharp
public class GetAllCustomersResponse
{
    public List<Customer> Cutomers { get; set; }
}
```

##Send a query
```csharp
public async Task<GetAllCustomersResponse> SendQuery(GetAllCustomersQuery query)
{
    return await _bus.SendAsync(query);
}
```

##Handle a query
```csharp
public class GetAllCustomersHandler : IHandleQueries<GetAllCustomersQuery, GetAllCustomersResponse>
{
    public async Task<GetAllCustomersResponse> HandleAsync(GetAllCustomersQuery query)
    {
        await Task.Delay(50);
        return new GetAllCustomersResponse
        {
            Cutomers = Db.Customers
        };
    }
}
```

##Set up an IoC container
###[LightInject](http://www.lightinject.net/) setup
```csharp
var serviceContainer = new ServiceContainer();
//Register all types that implements IHandleMessages and IHandleQueries
serviceContainer.RegisterAssembly(Assembly.GetExecutingAssembly(), (serviceType, implementingType) => serviceType.IsGenericType && (serviceType.GetGenericTypeDefinition() == typeof(IHandleMessages<>) || serviceType.GetGenericTypeDefinition() == typeof(IHandleQueries<,>)));

//Register the Bus
serviceContainer.Register<IBus>(sf => new Bus(sf.GetAllInstances), new PerContainerLifetime());
```

##Examples
See the [LightBus.Examples](https://github.com/kvarv/LightBus.Examples) repository
