using System;
using System.Collections.Generic;
using System.Linq;

namespace RestFluencing.Assertion.Rules
{
	public class HeaderKeyValueRule : AssertionRule
	{
		private string headerKey;
		private string headerValue;

		public HeaderKeyValueRule(string headerKey, string headerValue) : base("HeaderKeyValue")
		{
			this.headerKey = headerKey;
			this.headerValue = headerValue;
		}

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
	public class HeaderKeyRule : AssertionRule
	{
		private string headerKey;

		public HeaderKeyRule(string headerKey) : base("HeaderKey")
		{
			this.headerKey = headerKey;
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (!context.Response.Headers.ContainsKey(headerKey))
			{
				yield return new AssertionResult(this, $"Expected header {headerKey} to exist but was not found");
			}
		}
	}
	public class HeaderAssertRule : AssertionRule
	{
		private string headerKey;
		private readonly Func<IEnumerable<string>, bool> _assertion;
		private readonly string _message;

		public HeaderAssertRule(string headerKey, Func<IEnumerable<string>, bool> assertion, string message) : base("HeaderAssertRule")
		{
			this.headerKey = headerKey;
			_assertion = assertion;
			_message = message;
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (context.Response.Headers.ContainsKey(headerKey) && !_assertion(context.Response.Headers[headerKey]))
			{
				yield return new AssertionResult(this, $"Expected header rule on {headerKey} failed: {_message}");
			}
		}
	}
}