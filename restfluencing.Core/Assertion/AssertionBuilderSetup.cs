using System;
using System.Collections.Generic;
using restfluencing.Assertion.Rules;
using restfluencing.Assertion.Rules.Json;

namespace restfluencing.Assertion
{
	public static class AssertionBuilderSetup
	{
		public static AssertionBuilder ReturnsStatus(this AssertionBuilder builder, HttpStatusCode code)
		{
			builder.OnlyOneRuleOf(new HttpStatusRule(code));
			return builder;
		}

		public static AssertionBuilder HasHeader(this AssertionBuilder builder, string headerKey)
		{
			builder.AddRule(new HeaderKeyRule(headerKey));
			return builder;
		}

		public static AssertionBuilder HasHeaderValue(this AssertionBuilder builder, string headerKey, string headerValue)
		{
			builder.AddRule(new HeaderKeyValueRule(headerKey, headerValue));
			return builder;
		}

		public static AssertionBuilder HasHeader(this AssertionBuilder builder, string headerKey, Func<IEnumerable<string>, bool> assertionRule, string message)
		{
			builder.AddRule(new HeaderAssertRule(headerKey, assertionRule, message));
			return builder;
		}

		public static AssertionBuilder Returns<T>(this AssertionBuilder builder, Func<T, bool> expression, string error)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression, error));
			return builder;
		}

		public static AssertionBuilder Returns<T>(this AssertionBuilder builder, Func<T, bool> expression)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression));
			return builder;
		}

		public static AssertionBuilder ReturnsData(this AssertionBuilder builder, object model)
		{
			builder.AddRule(new JsonModelAssertionRule(model));
			return builder;
		}

		public static AssertionBuilder HasJsonSchema<T>(this AssertionBuilder builder)
		{
			var schemaRule = new JsonModelSchemaAssertionRule<T>();
			builder.AddRule(schemaRule);
			return builder;
		}
	}
}