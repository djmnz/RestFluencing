using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	public abstract class AssertionRule
	{
		protected AssertionRule(string ruleName)
		{
			RuleName = ruleName;
		}
		public string RuleName { get; }
		public abstract IEnumerable<AssertionResult> Assert(AssertionContext context);
	}
}