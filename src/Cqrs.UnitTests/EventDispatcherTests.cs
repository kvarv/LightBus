using Cqrs.Events;
using Xunit;

namespace Cqrs.UnitTests
{
	public class EventDispatcherTests
	{
		[Fact]
		public void Should_route_event_to_event_handlers_and_registered_handlers()
		{
			var eventDispatcher = new EventDispatcher();
			Route<TestEvent>.To<TestEventHandler>();
			Route<TestEvent>.To<TestEventHandler2>();
			eventDispatcher.RegisterHandler<TestEvent>(x => x.NumberOfTimesHandled++);
			var testMessage = new TestEvent();

			eventDispatcher.Dispatch(testMessage);

			testMessage.NumberOfTimesHandled.ShouldBeEqualTo(3);
		}
	}
}