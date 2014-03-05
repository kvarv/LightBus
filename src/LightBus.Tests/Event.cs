namespace LightBus.Tests
{
    public class Event : IEvent
    {
        public int NumberOfTimesHandled { get; set; }
    }

    public class EventWithCommand : IEvent
    {
        public Command Command { get; set; }
    }
}