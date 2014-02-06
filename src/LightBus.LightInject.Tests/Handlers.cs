namespace LightBus.LightInject.Tests
{
    public class TestCommandHandler : IHandleMessages<TestCommand>
    {
        public void Handle(TestCommand command)
        {
        }
    }

    public class TestCommand : ICommand
    {
    }

    public class TestEventHandler : IHandleMessages<TestEvent>
    {
        public void Handle(TestEvent @event)
        {
        }
    }

    public class TestEvent : IEvent
    {
    }

    public class TestRequestHandler : IHandleRequests<TestRequest, TestResponse>
    {
        public TestResponse Handle(TestRequest command)
        {
            return new TestResponse();
        }
    }

    public class TestResponse
    {
        public bool IsHandled { get; set; }
    }

    public class TestRequest : IRequest<TestResponse>
    {
    }
}