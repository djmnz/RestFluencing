using System.Linq;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Assertion that throws an exception with the details of the execution result.
	/// </summary>
	public class ExceptionAssertion : IAssertion
	{
		/// <summary>
		/// Asserts the result by throwing an <see cref="AssertionFailedException"/>
		/// </summary>
		/// <param name="result"></param>
		public void Assert(ExecutionResult result)
		{
			throw new AssertionFailedException(result);
		}
	}
}