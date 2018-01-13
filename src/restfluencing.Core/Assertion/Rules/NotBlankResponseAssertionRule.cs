using System.Collections.Generic;

namespace restfluencing.Assertion.Rules
{
	/// <summary>
	/// Assertion rule to check if the response is blank.
	/// </summary>
	public class NotBlankResponseAssertionRule : AssertionRule
	{
		private readonly string _error;

		public NotBlankResponseAssertionRule(string error) : base("NotBlankResponse")
		{
			_error = error ?? "Response body was blank.";
		}
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (string.IsNullOrEmpty(context.Response.Content))
			{
				yield return new AssertionResult(this, _error);
			}
		}
	}
}