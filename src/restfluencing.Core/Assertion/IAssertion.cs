namespace RestFluencing.Assertion
{
	/// <summary>
	/// Interface to process the results of the assertion.
	/// </summary>
	public interface IAssertion
	{
		void Assert(ExecutionResult result);
	}
}