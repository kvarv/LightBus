namespace LightBus.Tests
{
    using LightBus;

    public class TestCommand : ICommand
	{
		public bool IsHandled { get;  set; }
	}
}