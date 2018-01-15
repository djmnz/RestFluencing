using System;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Assertion that writes into the Console the results.
	/// </summary>
	public class ConsoleAssertion : IAssertion
	{
		public void Assert(ExecutionResult result)
		{
			Console.WriteLine(AssertionFailedException.GetFullMessage(result));
		}
	}
}