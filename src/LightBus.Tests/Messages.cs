namespace LightBus.Tests
{
    public class Command : IRequest<Unit>
    {
        public bool IsHandled { get; set; }
    }

    public class Event : IEvent
    {
        public int NumberOfTimesHandled { get; set; }
    }

    public class EventWithCommand : IEvent
    {
        public Command Command { get; set; }
    }

    public class AsyncCommand : IRequest<Unit>
    {
        public bool IsHandled { get; set; }
    }

    public class CommandWithException : IRequest<Unit>
    {
    }

    public class EventWithException : IEvent
    {
    }

    public class Query : IRequest<Response>
    {
    }

    public class QueryWithExcepetion : IRequest<Response>
    {
    }
}