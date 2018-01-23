using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Assertion rule to check if the response is blank.
	/// </summary>
	public class NotBlankResponseAssertionRule : AssertionRule
	{
		private readonly string _error;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="error"></param>
		public NotBlankResponseAssertionRule(string error) : base("NotBlankResponse")
		{
			_error = error ?? "Response body was blank.";
		}
		/// <summary>
		/// Asserts that the response is not blank.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (string.IsNullOrEmpty(context.Response.Content))
			{
				yield return new AssertionResult(this, _error);
			}
		}
	}
}