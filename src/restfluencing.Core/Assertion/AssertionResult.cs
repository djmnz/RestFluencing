using restfluencing.Assertion.Rules;

namespace restfluencing.Assertion
{
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