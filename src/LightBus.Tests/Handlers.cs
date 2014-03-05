namespace LightBus.Tests
{
    public class CommandHandler : IHandleMessages<Command>
    {
        public void Handle(Command command)
        {
            command.IsHandled = true;
        }
    }

    public class AnotherCommandHandler : IHandleMessages<Command>
    {
        public void Handle(Command command)
        {
            command.IsHandled = true;
        }
    }

    public class CommandHandlerThatSendsAnEvent : IHandleMessages<Command>
    {
        private readonly IBus _bus;

        public CommandHandlerThatSendsAnEvent(IBus bus)
        {
            _bus = bus;
        }

        public void Handle(Command command)
        {
            _bus.Publish(new EventWithCommand{Command = command});
        }
    }

    public class EventHandler : IHandleMessages<Event>
    {
        public void Handle(Event @event)
        {
            @event.NumberOfTimesHandled++;
        }
    }

    public class AnotherEventHandler : IHandleMessages<Event>
    {
        public void Handle(Event @event)
        {
            @event.NumberOfTimesHandled++;
        }
    }

    public class EventWithCommandHandler : IHandleMessages<EventWithCommand>
    {
        public void Handle(EventWithCommand @event)
        {
            @event.Command.IsHandled = true;
        }
    }

    public class QueryHandler : IHandleQueries<Query, Response>
    {
        public Response Handle(Query query)
        {
            return new Response {IsHandled = true};
        }
    }

    public class AnotherQueryHandler : IHandleQueries<Query, Response>
    {
        public Response Handle(Query query)
        {
            return new Response {IsHandled = true};
        }
    }
}