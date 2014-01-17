namespace LightBus.Tests
{
    using LightBus;

    public class TestCommandHandler : IHandle<TestCommand>
	{
		public void Handle(TestCommand command)
		{
			command.IsHandled = true;
		}
	}

    //public class TestCommandHandler2 : IHandle<TestCommand>
    //{
    //    public void Handle(TestCommand command)
    //    {
    //        command.IsHandled = true;
    //    }
    //}

	public class TestEventHandler : IHandle<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			@event.NumberOfTimesHandled++;
		}
	}

	public class TestEventHandler2 : IHandle<TestEvent>
	{
		public void Handle(TestEvent @event)
		{
			@event.NumberOfTimesHandled++;
		}
	}
}