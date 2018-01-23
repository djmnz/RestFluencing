using System;
using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Base type that define what an AssertionRule should have.
	/// </summary>
	public abstract class AssertionRule
	{
		/// <summary>
		/// Constructor to enforce the rule name
		/// </summary>
		/// <param name="ruleName"></param>
		protected AssertionRule(string ruleName)
		{
			RuleName = ruleName ?? throw new ArgumentNullException(nameof(ruleName));
		}

		/// <summary>
		/// Friendly name of the rule.
		/// </summary>
		public string RuleName { get; }

		/// <summary>
		/// Logic of the asssertion against the context
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public abstract IEnumerable<AssertionResult> Assert(AssertionContext context);
	}
}