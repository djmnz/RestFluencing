using RestFluencing.Assertion.Rules;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Details of an assertion failure.
	/// </summary>
	public class AssertionResult
	{

		public AssertionResult(AssertionRule rule, string message)
		{
			CausedBy = rule;
			ErrorMessage = message;
		}

		public AssertionRule CausedBy { get; }
		public string ErrorMessage { get; }
	}
}