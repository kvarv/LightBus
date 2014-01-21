namespace LightBus.Tests
{
    using LightBus;

    public class TestEvent : IEvent
	{
		public int NumberOfTimesHandled { get; set; }
	}
}