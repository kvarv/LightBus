namespace LightBus.Tests
{
    public class TestCommandHandler : IHandleMessages<TestCommand>
    {
        public void Handle(TestCommand command)
        {
            command.IsHandled = true;
        }
    }

    public class AnotherTestCommandHandler : IHandleMessages<TestCommand>
    {
        public void Handle(TestCommand command)
        {
            command.IsHandled = true;
        }
    }

    public class TestEventHandler : IHandleMessages<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
            @event.NumberOfTimesHandled++;
        }
    }

    public class TestEventHandler2 : IHandleMessages<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
            @event.NumberOfTimesHandled++;
        }
    }

    public class TestQueryHandler : IHandleQueries<TestQuery, TestResponse>
    {
        public TestResponse Handle(TestQuery query)
        {
            return new TestResponse {IsHandled = true};
        }
    }

    public class AnotherTestQueryHandler : IHandleQueries<TestQuery, TestResponse>
    {
        public TestResponse Handle(TestQuery query)
        {
            return new TestResponse {IsHandled = true};
        }
    }
}