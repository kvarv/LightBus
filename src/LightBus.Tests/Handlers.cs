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

    public class TestRequestHandler : IHandleRequests<TestRequest, TestResponse>
    {
        public TestResponse Handle(TestRequest command)
        {
            return new TestResponse {IsHandled = true};
        }
    }

    public class AnotherTestRequestHandler : IHandleRequests<TestRequest, TestResponse>
    {
        public TestResponse Handle(TestRequest command)
        {
            return new TestResponse {IsHandled = true};
        }
    }
}