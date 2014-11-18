using System;
using System.Threading.Tasks;

namespace LightBus.Tests
{
    public class Async
    {
        public class CommandHandler : IHandleRequestsAsync<Command, Unit>
        {
            public Task<Unit> Handle(Command command)
            {
                command.IsHandled = true;
                return TaskExt.FromResult(Unit.Value);
            }
        }

        public class AnotherCommandHandler : IHandleRequestsAsync<Command, Unit>
        {
            public Task<Unit> Handle(Command command)
            {
                command.IsHandled = true;
                return TaskExt.FromResult(Unit.Value);
            }
        }

        public class MessageHandler : IHandleEventsAsync<IEvent>
        {
            public bool IsHandled { get; set; }

            public Task Handle(IEvent @event)
            {
                IsHandled = true;
                return TaskExt.FromResult();
            }
        }

        public class CommandHandlerThatSendsAnEvent : IHandleRequestsAsync<Command, Unit>
        {
            private readonly IMediator _mediator;

            public CommandHandlerThatSendsAnEvent(IMediator mediator)
            {
                _mediator = mediator;
            }

            public async Task<Unit> Handle(Command command)
            {
                await _mediator.PublishAsync(new EventWithCommand { Command = command });
                return Unit.Value;
            }
        }

        public class CommandHandlerWithDelay : IHandleRequestsAsync<AsyncCommand, Unit>
        {
            public Task<Unit> Handle(AsyncCommand command)
            {
                return TaskExt.Delay(50).ContinueWith(task =>
                {
                    command.IsHandled = true;
                    return Unit.Value;
                });
            }
        }

        public class CommandHandlerThatThrowException : IHandleRequestsAsync<CommandWithException, Unit>
        {
            public Task<Unit> Handle(CommandWithException message)
            {
                throw new InvalidOperationException();
            }
        }

        public class EventHandler : IHandleEventsAsync<Event>
        {
            public Task Handle(Event @event)
            {
                @event.NumberOfTimesHandled++;
                return TaskExt.FromResult();
            }
        }

        public class AnotherEventHandler : IHandleEventsAsync<Event>
        {
            public Task Handle(Event @event)
            {
                @event.NumberOfTimesHandled++;
                return TaskExt.FromResult();
            }
        }

        public class EventWithCommandHandler : IHandleEventsAsync<EventWithCommand>
        {
            public Task Handle(EventWithCommand @event)
            {
                @event.Command.IsHandled = true;
                return TaskExt.FromResult();
            }
        }

        public class QueryHandler : IHandleRequestsAsync<Query, Response>
        {
            public Task<Response> Handle(Query query)
            {
                return TaskExt.FromResult(new Response { IsHandled = true });
            }
        }

        public class AnotherQueryHandler : IHandleRequestsAsync<Query, Response>
        {
            public Task<Response> Handle(Query query)
            {
                return TaskExt.FromResult(new Response { IsHandled = true });
            }
        }

        public class EventWithExceptionHandler : IHandleEventsAsync<EventWithException>
        {
            public Task Handle(EventWithException @event)
            {
                throw new InvalidOperationException();
            }
        }

        public class QueryWithExceptionHandler : IHandleRequestsAsync<QueryWithExcepetion, Response>
        {
            public Task<Response> Handle(QueryWithExcepetion query)
            {
                throw new InvalidOperationException();
            }
        }
    }
}