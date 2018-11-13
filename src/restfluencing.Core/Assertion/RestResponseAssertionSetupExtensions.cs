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
		public static RestResponse ReturnsStatus(this RestResponse response, HttpStatusCode code)
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
		public static RestResponse HasHeader(this RestResponse response, string headerKey)
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
		public static RestResponse HasHeaderValue(this RestResponse response, string headerKey, string headerValue)
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
		public static RestResponse HasHeader(this RestResponse response, string headerKey, Expression<Func<IEnumerable<string>, bool>> assertionRule, string message)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new HeaderAssertRule(headerKey, assertionRule, message));
			return response;
		}

        /// <summary>
        /// Allows an Action to be called on the response object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="action">Action to call on the response object.</param>
        /// <returns></returns>
        public static RestResponse Action<T>(this RestResponse response, Action<T> action)
        {
            if (response == null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            response.AddRule(new ActionRule<T>(action));
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
        public static RestResponse Returns<T>(this RestResponse response, Expression<Func<T, bool>> expression, string errorReason)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new ExpressionAssertionRule<T>(expression, errorReason));
			return response;
		}

		/// <summary>
		/// Verifies that the response body contains a value but based on a dynamic type. For explicit clarity the error reason must be provided.
		/// </summary>
		/// <param name="response"></param>
		/// <param name="expression">Dynamic expression to be validated</param>
		/// <param name="errorReason">Explicit error</param>
		/// <returns></returns>
		public static RestResponse ReturnsDynamic(this RestResponse response, Func<dynamic, bool> expression, string errorReason)
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
		public static RestResponse Returns<T>(this RestResponse response, Expression<Func<T, bool>> expression)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			response.AddRule(new ExpressionAssertionRule<T>(expression));
			return response;
		}

		/// <summary>
		/// Verifies that the response has an empty body.
		/// </summary>
		/// <param name="response">Response to add the new assertion rule</param>
		/// <param name="errorReason">Reason why it should be empty</param>
		/// <returns></returns>
		public static RestResponse ReturnsEmptyContent(this RestResponse response, string errorReason)
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
		public static RestResponse ReturnsEmptyContent(this RestResponse response)
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
		public static RestResponse ReturnsContent(this RestResponse response, string errorReason)
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
		public static RestResponse ReturnsContent(this RestResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response));
			}
			return response.ReturnsContent(null);
		}

	}
}