using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using RestFluencing.Assertion.Rules;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Extensions for the response
	/// </summary>
	public static class RestResponseAssertionSetupExtensions
	{
		/// <summary>
		/// Asserts that the response returned with a specific status code
		/// </summary>
		/// <param name="response"></param>
		/// <param name="code">Expected status code</param>
		/// <returns></returns>
		public static IRestResponse ReturnsStatus(this IRestResponse response, HttpStatusCode code)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}

			response.OnlyOneRuleOf(new HttpStatusRule(code));
			return response;
		}


		/// <summary>
		/// Asserts that the response returned with a specific header key
		/// </summary>
		/// <param name="response"></param>
		/// <param name="headerKey">Expected header key</param>
		/// <returns></returns>
		public static IRestResponse HasHeader(this IRestResponse response, string headerKey)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}

			response.AddRule(new HeaderKeyRule(headerKey));

			return response;
		}

		/// <summary>
		/// Asserts that the response returned with a header containing the value
		/// </summary>
		/// <param name="response"></param>
		/// <param name="headerKey">Expected header key</param>
		/// <param name="headerValue">Expected header value</param>
		/// <returns></returns>
		public static IRestResponse HasHeaderValue(this IRestResponse response, string headerKey, string headerValue)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new HeaderKeyValueRule(headerKey, headerValue));
			return response;
		}

		/// <summary>
		/// Asserts that the expression matches the values in the header key
		/// </summary>
		/// <param name="response"></param>
		/// <param name="headerKey">Header key</param>
		/// <param name="assertionRule">Expression rule</param>
		/// <param name="message">Explicit error message provided when the assertion rule fails.</param>
		/// <returns></returns>
		public static IRestResponse HasHeader(this IRestResponse response, string headerKey, Expression<Func<IEnumerable<string>, bool>> assertionRule, string message)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new HeaderAssertRule(headerKey, assertionRule, message));
			return response;
		}



		/// <summary>
		/// Verifies that the response body contains a value but based on a dynamic type. For explicit clarity the error reason must be provided.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="expression">Dynamic expression to be validated</param>
		/// <param name="errorReason">Explicit error</param>
		/// <returns></returns>
		public static IRestResponse ReturnsDynamic(this IRestResponse response, Func<dynamic, bool> expression, string errorReason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new DynamicExpressionAssertionRule(expression, errorReason));
			return response;
		}

		/// <summary>
		/// Verifies that the response body has the matching expression predicate.
		/// </summary>
		/// <typeparam name="T">Type to deserialise the response body as</typeparam>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <param name="expression">Expression to validate</param>
		/// <returns></returns>
		public static IRestResponse Returns<T>(this IRestResponse response, Expression<Func<T, bool>> expression)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new ExpressionAssertionRule<T>(expression));
			return response;
		}

		/// <summary>
		/// Verifies that the intended behaviour expressed on the expression is met
		/// </summary>
		/// <typeparam name="T">Type to deserialise the response body as</typeparam>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <param name="lambda">Expression to validate</param>
		/// <param name="reason">Explain the reason why the assertion failed.</param>
		/// <returns></returns>
		public static IRestResponse ReturnsModel<T>(this IRestResponse response, Func<T, bool> lambda, string reason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new InlineAssertionRule<T>(lambda, reason));
			return response;
		}

		/// <summary>
		/// Asserts that the response content contains the expected value based on the type provided
		/// </summary>
		/// <typeparam name="T">Expected response Type</typeparam>
		/// <param name="response"></param>
		/// <param name="expression">Expression to validate</param>
		/// <param name="errorReason">Explicit error message</param>
		/// <returns></returns>
		public static IRestResponse Returns<T>(this IRestResponse response, Expression<Func<T, bool>> expression, string errorReason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new ExpressionAssertionRule<T>(expression, errorReason));
			return response;
		}

		/// <summary>
		/// Verifies that the intended behaviour expressed on the expression is met
		/// </summary>
		/// <typeparam name="T">Type to deserialise the response body as</typeparam>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <param name="expression">Expression to validate</param>
		/// <param name="reason">Explain the reason why the assertion failed with additional details about the model if required.</param>
		/// <returns></returns>
		public static IRestResponse Returns<T>(this IRestResponse response, Func<T, bool> expression, Func<T, string> reason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new InlineAssertionRule<T>(expression, reason));
			return response;
		}

		/// <summary>
		/// Verifies that the response has an empty body.
		/// </summary>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <param name="errorReason">Reason why it should be empty</param>
		/// <returns></returns>
		public static IRestResponse ReturnsEmptyContent(this IRestResponse response, string errorReason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}

			response.AddRule(new BlankResponseAssertionRule(errorReason));
			return response;
		}

		/// <summary>
		/// Verifies that the response has an empty body.
		/// </summary>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <returns></returns>
		public static IRestResponse ReturnsEmptyContent(this IRestResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			return response.ReturnsEmptyContent(null);
		}

		/// <summary>
		/// Verifies that the response returned anything in the body.
		/// </summary>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <param name="errorReason">Reason why it should have content</param>
		/// <returns></returns>
		public static IRestResponse ReturnsContent(this IRestResponse response, string errorReason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new NotBlankResponseAssertionRule(errorReason));
			return response;
		}

		/// <summary>
		/// Verifies that the response returned anything in the body.
		/// </summary>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <returns></returns>
		public static IRestResponse ReturnsContent(this IRestResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			return response.ReturnsContent(null);
		}

	}
}