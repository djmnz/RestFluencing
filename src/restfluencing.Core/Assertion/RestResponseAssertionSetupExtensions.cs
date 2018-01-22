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
		/// <summary>
		/// Asserts that the response returned with a specific status code
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="code">Expected status code</param>
		/// <returns></returns>
		public static RestResponse ReturnsStatus(this RestResponse builder, HttpStatusCode code)
		{
			builder.OnlyOneRuleOf(new HttpStatusRule(code));
			return builder;
		}


		/// <summary>
		/// Asserts that the response returned with a specific header key
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="headerKey">Expected header key</param>
		/// <returns></returns>
		public static RestResponse HasHeader(this RestResponse builder, string headerKey)
		{
			builder.AddRule(new HeaderKeyRule(headerKey));
			return builder;
		}

		/// <summary>
		/// Asserts that the response returned with a header containing the value
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="headerKey">Expected header key</param>
		/// <param name="headerValue">Expected header value</param>
		/// <returns></returns>
		public static RestResponse HasHeaderValue(this RestResponse builder, string headerKey, string headerValue)
		{
			builder.AddRule(new HeaderKeyValueRule(headerKey, headerValue));
			return builder;
		}

		/// <summary>
		/// Asserts that the expression matches the values in the header key
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="headerKey">Header key</param>
		/// <param name="assertionRule">Expression rule</param>
		/// <param name="message">Explicit error message provided when the assertion rule fails.</param>
		/// <returns></returns>
		public static RestResponse HasHeader(this RestResponse builder, string headerKey, Expression<Func<IEnumerable<string>, bool>> assertionRule, string message)
		{
			builder.AddRule(new HeaderAssertRule(headerKey, assertionRule, message));
			return builder;
		}

		/// <summary>
		/// Asserts that the response content contains the expected value based on the type provided
		/// </summary>
		/// <typeparam name="T">Expected response Type</typeparam>
		/// <param name="builder"></param>
		/// <param name="expression">Expression to validate</param>
		/// <param name="error">Explicit error message</param>
		/// <returns></returns>
		public static RestResponse Returns<T>(this RestResponse builder, Expression<Func<T, bool>> expression, string error)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression, error));
			return builder;
		}

		/// <summary>
		/// Verifies that the response body contains a value but based on a dynamic type. For explicit clarity and reasoning the error reason must be provided.
		/// </summary>
		/// <param name="builder"></param>
		/// <param name="expression">Dynamic expression to be validated</param>
		/// <param name="error">Explicit error</param>
		/// <returns></returns>
		public static RestResponse ReturnsDynamic(this RestResponse builder, Func<dynamic, bool> expression, string error)
		{
			builder.AddRule(new DynamicExpressionAssertionRule(expression, error));
			return builder;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="builder"></param>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static RestResponse Returns<T>(this RestResponse builder, Expression<Func<T, bool>> expression)
		{
			builder.AddRule(new ExpressionAssertionRule<T>(expression));
			return builder;
		}

		public static RestResponse ReturnsEmptyContent(this RestResponse builder, string error)
		{
			builder.AddRule(new BlankResponseAssertionRule(error));
			return builder;
		}

		public static RestResponse ReturnsEmptyContent(this RestResponse builder)
		{
			return builder.ReturnsEmptyContent(null);
		}

		public static RestResponse ReturnsContent(this RestResponse builder, string error)
		{
			builder.AddRule(new NotBlankResponseAssertionRule(error));
			return builder;
		}

		public static RestResponse ReturnsContent(this RestResponse builder)
		{
			return builder.ReturnsContent(null);
		}

	}
}