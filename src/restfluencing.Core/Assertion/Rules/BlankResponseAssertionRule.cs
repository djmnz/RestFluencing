using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Assertion rule to check if the response is not blank.
	/// </summary>
	public class BlankResponseAssertionRule : AssertionRule
	{
		private readonly string _error;

		public BlankResponseAssertionRule(string error) : base("BlankResponse")
		{
			_error = error ?? "Response was not blank. Response body: {0}";
		}
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (!string.IsNullOrEmpty(context.Response.Content))
			{
				yield return new AssertionResult(this, string.Format(_error, context.Response.Content));
			}
		}
	}
}