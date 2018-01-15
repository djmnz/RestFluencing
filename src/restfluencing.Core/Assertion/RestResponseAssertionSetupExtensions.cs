using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using RestFluencing.Assertion.Rules;

namespace RestFluencing.Assertion
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

		public static RestResponse Returns<T>(this RestResponse builder, Expression<Func<T, bool>> expression, string error)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression, error));
			return builder;
		}

		public static RestResponse Returns(this RestResponse builder, Func<dynamic, bool> expression, string error)
		{
			builder.AddRule(new DynamicExpressionAssertionRule(expression, error));
			return builder;
		}

		public static RestResponse Returns<T>(this RestResponse builder, Expression<Func<T, bool>> expression)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression));
			return builder;
		}

		public static RestResponse IsEmpty(this RestResponse builder, string error)
		{
			builder.AddRule(new BlankResponseAssertionRule(error));
			return builder;
		}

		public static RestResponse IsEmpty(this RestResponse builder)
		{
			return builder.IsEmpty(null);
		}

		public static RestResponse IsNotEmpty(this RestResponse builder, string error)
		{
			builder.AddRule(new NotBlankResponseAssertionRule(error));
			return builder;
		}

		public static RestResponse IsNotEmpty(this RestResponse builder)
		{
			return builder.IsNotEmpty(null);
		}

	}
}