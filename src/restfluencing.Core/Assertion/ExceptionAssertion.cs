using System.Linq;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Assertion that throws an exception with the details of the execution result.
	/// </summary>
	public class ExceptionAssertion : IAssertion
	{
		public void Assert(ExecutionResult result)
		{
			throw new AssertionFailedException(result);
		}
	}
}