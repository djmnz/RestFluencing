using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Assertion rule to validate that the response has the specified value on the header.
	/// </summary>
	public class HeaderAssertRule : AssertionRule
	{
		private string headerKey;
		private readonly Func<IEnumerable<string>, bool> _assertion;
		private readonly string _assertBody;
		private readonly string _message;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="headerKey"></param>
		/// <param name="expression"></param>
		/// <param name="message"></param>
		public HeaderAssertRule(string headerKey, Expression<Func<IEnumerable<string>, bool>> expression, string message) : base("HeaderAssertRule")
		{
			this.headerKey = headerKey;
			_assertion = expression.Compile();
			_assertBody = expression.Body.ToString();
			_message = message;
		}

		/// <summary>
		/// Asserts that the assertion function is true against the header key value.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			// note we don't validate whether the key exists as there is another assertion rule that does that.
			if (context.Response.Headers.ContainsKey(headerKey) && !_assertion(context.Response.Headers[headerKey]))
			{
				yield return new AssertionResult(this, $"Expected rule {_assertBody} on header key {headerKey} failed: {_message}");
			}
		}
	}
}