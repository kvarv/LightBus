using System;
using System.Threading.Tasks;

namespace LightBus.Tests
{
    public class CommandHandler : IHandleMessages<Command>
    {
        public System.Threading.Tasks.Task HandleAsync(Command command)
        {
            command.IsHandled = true;
            return Task.FromResult();
        }
    }

    public class AnotherCommandHandler : IHandleMessages<Command>
    {
        public System.Threading.Tasks.Task HandleAsync(Command command)
        {
            command.IsHandled = true;
            return Task.FromResult();
        }
    }

    public class CommandHandlerThatSendsAnEvent : IHandleMessages<Command>
    {
        private readonly IBus _bus;

        public CommandHandlerThatSendsAnEvent(IBus bus)
        {
            _bus = bus;
        }

        public System.Threading.Tasks.Task HandleAsync(Command command)
        {
            return _bus.PublishAsync(new EventWithCommand{Command = command});
        }
    }

    public class AsyncCommandHandler : IHandleMessages<AsyncCommand>
    {
        public System.Threading.Tasks.Task HandleAsync(AsyncCommand command)
        {            
            return Task.Delay(50).ContinueWith(task => command.IsHandled = true);
        }
    }

    public class EventHandler : IHandleMessages<Event>
    {
        public System.Threading.Tasks.Task HandleAsync(Event @event)
        {
            @event.NumberOfTimesHandled++;
            return Task.FromResult();
        }
    }

    public class AnotherEventHandler : IHandleMessages<Event>
    {
        public System.Threading.Tasks.Task HandleAsync(Event @event)
        {
            @event.NumberOfTimesHandled++;
            return Task.FromResult();
        }
    }

    public class EventWithCommandHandler : IHandleMessages<EventWithCommand>
    {
        public System.Threading.Tasks.Task HandleAsync(EventWithCommand @event)
        {
            @event.Command.IsHandled = true;
            return Task.FromResult();
        }
    }

    public class QueryHandler : IHandleQueries<Query, Response>
    {
        public Task<Response> HandleAsync(Query query)
        {
            return Task.FromResult(new Response { IsHandled = true });
        }
    }

    public class AnotherQueryHandler : IHandleQueries<Query, Response>
    {
        public Task<Response> HandleAsync(Query query)
        {
            return Task.FromResult(new Response { IsHandled = true });
        }
    }
}