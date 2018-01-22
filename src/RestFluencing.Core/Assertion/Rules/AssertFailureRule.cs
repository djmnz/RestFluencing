using System.Collections.Generic;
using System.Linq;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Rule used to assert that there was no failure result.
	/// </summary>
	internal class AssertFailureRule : AssertionRule
	{
		public AssertFailureRule() : base("AssertFailure")
		{
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			// the logic for this assertion rule is not done here, but the in AssertFailure method
			return Enumerable.Empty<AssertionResult>();
		}
	}
}