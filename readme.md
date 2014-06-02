[![Build status](https://ci.appveyor.com/api/projects/status/jx72f65gkr8m8mps)](https://ci.appveyor.com/project/kvarv/lightbus)
#LightBus

**LightBus** is a lightweight in-process bus, which pretty much makes it an implementation of the mediator pattern. **LightBus**, insipired by [NServiceBus](http://www.particular.net/) and the CQRS pattern, allows you to send messages (commands, events, queries) to registered recipients in a decoupled manner. Recipients, i.e. message handlers, are defined by marker interfaces and lets you encapsulate business operations in a single class, which in turn is registered and instantiated by an IoC container. 

Typical use case is directly behind your service boundary, for example server side behind a service interface like ASP.NET Web Api, ASP.NET MVC, Nancy, ServiceStack, WCF etc. 

**LightBus** is all async and uses the Task Parallell Library which means that you could use async/await on .NET 4.5. Still, **LightBus** targets .NET 4.0

##Installation through NuGet
```PM> Install-Package LightBus```

##How do I get started?
Check out the [wiki](https://github.com/kvarv/LightBus/wiki)!
