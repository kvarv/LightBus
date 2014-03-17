namespace LightBus.Tests
{
    public class Command : ICommand
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

    public class AsyncCommand : ICommand
    {
        public bool IsHandled { get; set; }
    }
}   