using System;
using System.Threading.Tasks;

namespace LightBus.Tests
{
    public class CommandHandler : IHandleRequests<Command, Unit>
    {
        public Unit Handle(Command command)
        {
            command.IsHandled = true;
            return Unit.Value;
        }
    }

    public class AnotherCommandHandler : IHandleRequests<Command, Unit>
    {
        public Unit Handle(Command command)
        {
            command.IsHandled = true;
            return Unit.Value;
        }
    }

    public class MessageHandler : IHandleEvents<IEvent>
    {
        public bool IsHandled { get; set; }

        public void Handle(IEvent @event)
        {
            IsHandled = true;
        }
    }

    public class CommandHandlerThatSendsAnEvent : IHandleRequests<Command, Unit>
    {
        private readonly IMediator _mediator;

        public CommandHandlerThatSendsAnEvent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Unit Handle(Command command)
        {
            _mediator.Publish(new EventWithCommand { Command = command });
            return Unit.Value;
        }
    }

    public class CommandHandlerThatThrowException : IHandleRequests<CommandWithException, Unit>
    {
        public Unit Handle(CommandWithException message)
        {
            throw new InvalidOperationException();
        }
    }

    public class EventHandler : IHandleEvents<Event>
    {
        public void Handle(Event @event)
        {
            @event.NumberOfTimesHandled++;
        }
    }

    public class AnotherEventHandler : IHandleEvents<Event>
    {
        public void Handle(Event @event)
        {
            @event.NumberOfTimesHandled++;
        }
    }

    public class EventWithCommandHandler : IHandleEvents<EventWithCommand>
    {
        public void Handle(EventWithCommand @event)
        {
            @event.Command.IsHandled = true;
        }
    }

    public class QueryHandler : IHandleRequests<Query, Response>
    {
        public Response Handle(Query query)
        {
            return new Response { IsHandled = true };
        }
    }

    public class AnotherQueryHandler : IHandleRequests<Query, Response>
    {
        public Response Handle(Query query)
        {
            return new Response { IsHandled = true };
        }
    }

    public class EventWithExceptionHandler : IHandleEvents<EventWithException>
    {
        public void Handle(EventWithException @event)
        {
            throw new InvalidOperationException();
        }
    }

    public class QueryWithExceptionHandler : IHandleRequests<QueryWithExcepetion, Response>
    {
        public Response Handle(QueryWithExcepetion query)
        {
            throw new InvalidOperationException();
        }
    }
}