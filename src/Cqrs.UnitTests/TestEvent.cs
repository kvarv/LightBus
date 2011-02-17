using Cqrs.Events;

namespace Cqrs.UnitTests
{
	public class TestEvent : Event
	{
		public int NumberOfTimesHandled { get; set; }
	}
}