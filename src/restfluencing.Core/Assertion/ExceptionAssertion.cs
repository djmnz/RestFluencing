namespace restfluencing.Assertion
{
	/// <summary>
	/// Assertion that throws an exception when rules were violated.
	/// </summary>
	public class ExceptionAssertion : IAssertion
	{
		public void Assert(ExecutionResult result)
		{
			result.Assert();
		}
	}
}