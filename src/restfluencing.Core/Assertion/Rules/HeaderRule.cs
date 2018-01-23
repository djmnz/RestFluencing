using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Assertion rule to validate that the response has a header key with a specific value.
	/// </summary>
	public class HeaderKeyValueRule : AssertionRule
	{
		private string headerKey;
		private string headerValue;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="headerKey"></param>
		/// <param name="headerValue"></param>
		public HeaderKeyValueRule(string headerKey, string headerValue) : base("HeaderKeyValue")
		{
			this.headerKey = headerKey;
			this.headerValue = headerValue;
		}

		/// <summary>
		/// Asserts that the response contains the header and value.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (!context.Response.Headers.ContainsKey(headerKey))
			{
				yield return new AssertionResult(this, $"Expected header {headerKey} to exist but was not found");
			} else 
			if (!context.Response.Headers[headerKey].Contains(headerValue))
			{
				yield return new AssertionResult(this, $"Expected header {headerKey} to have value {headerValue}, found {string.Join(", ", context.Response.Headers[headerKey])}");
			}
		}
	}
}