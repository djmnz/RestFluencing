using System;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Assertion that writes into the Console the results.
	/// </summary>
	public class ConsoleAssertion : IAssertion
	{
		/// <summary>
		/// Asserts the result by writing to the console.
		/// </summary>
		/// <param name="result"></param>
		public void Assert(ExecutionResult result)
		{
			Console.WriteLine(AssertionFailedException.GetFullMessage(result));
		}
	}
}