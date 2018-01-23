using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Rule to assert that the response contains the header key specified.
	/// </summary>
	public class HeaderKeyRule : AssertionRule
	{

		private string headerKey;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="headerKey"></param>
		public HeaderKeyRule(string headerKey) : base("HeaderKey")
		{
			this.headerKey = headerKey;
		}

		/// <summary>
		/// Asserts that the response contains the specified headerkey
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (!context.Response.Headers.ContainsKey(headerKey))
			{
				yield return new AssertionResult(this, $"Expected header {headerKey} to exist but was not found");
			}
		}
	}
}