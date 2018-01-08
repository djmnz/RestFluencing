using System;
using System.Collections.Generic;
using restfluencing.Assertion.Rules;

namespace restfluencing.Assertion
{
	public static class RestResponseAssertionSetupExtensions
	{
		public static RestResponse ReturnsStatus(this RestResponse builder, HttpStatusCode code)
		{
			builder.OnlyOneRuleOf(new HttpStatusRule(code));
			return builder;
		}

		public static RestResponse HasHeader(this RestResponse builder, string headerKey)
		{
			builder.AddRule(new HeaderKeyRule(headerKey));
			return builder;
		}

		public static RestResponse HasHeaderValue(this RestResponse builder, string headerKey, string headerValue)
		{
			builder.AddRule(new HeaderKeyValueRule(headerKey, headerValue));
			return builder;
		}

		public static RestResponse HasHeader(this RestResponse builder, string headerKey, Func<IEnumerable<string>, bool> assertionRule, string message)
		{
			builder.AddRule(new HeaderAssertRule(headerKey, assertionRule, message));
			return builder;
		}

		public static RestResponse Returns<T>(this RestResponse builder, Func<T, bool> expression, string error)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression, error));
			return builder;
		}

		public static RestResponse Returns<T>(this RestResponse builder, Func<T, bool> expression)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression));
			return builder;
		}
	}
}