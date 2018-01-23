using System;
using RestFluencing.Assertion.Rules;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Details of an assertion failure.
	/// </summary>
	public class AssertionResult
	{
		/// <summary>
		/// Constructor to create the assertion result based on a rule and the explanation message
		/// </summary>
		/// <param name="rule">Rule that caused this result</param>
		/// <param name="message">Human friendly message that explains the result</param>
		public AssertionResult(AssertionRule rule, string message)
		{
			CausedBy = rule ?? throw new ArgumentNullException(nameof(rule));
			ErrorMessage = message ?? throw new ArgumentNullException(nameof(ErrorMessage));
		}

		/// <summary>
		/// Assertion rule that caused this result
		/// </summary>
		public AssertionRule CausedBy { get; }

		/// <summary>
		/// Reason why this result ocurred.
		/// </summary>
		public string ErrorMessage { get; }
	}
}