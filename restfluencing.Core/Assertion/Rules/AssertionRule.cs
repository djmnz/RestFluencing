using System.Collections.Generic;

namespace restfluencing.Assertion.Rules
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