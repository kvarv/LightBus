namespace Cqrs.UnitTests
{
	public abstract class Scenario
	{
		protected Scenario()
		{
			Given();
			When();
		}

		protected abstract void Given();
		protected abstract void When();
	}
}