using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestFluencing.Assertion;
using RestFluencing.Assertion.Rules;
using RestFluencing.Helpers;

namespace RestFluencing
{
	/// <summary>
	///     Extensions for validating the final results.
	/// </summary>
	public static class AssertionsExtensions
	{
		/// <summary>
		///     The response will be asserted and expected to have no errors.
		/// </summary>
		/// <param name="response">Result to validate</param>
		public static void ShouldPass(this RestResponse response)
		{
			if (response == null)
				throw new ArgumentNullException(nameof(response), ErrorMessages.NoResponse);
			response.Assert();
		}

		/// <summary>
		///     The result should contain no errors.
		/// </summary>
		/// <param name="result">Result to validate</param>
		public static void ShouldPass(this ExecutionResult result)
		{
			if (result == null)
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			result.ShouldPass(null);
		}

		/// <summary>
		///     The result is expected to have errors.
		/// </summary>
		/// <param name="result">Result to validate</param>
		public static void ShouldFail(this ExecutionResult result)
		{
			if (result == null)
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			result.ShouldFail(null);
		}

		/// <summary>
		///     The result is expected to have errors.
		/// </summary>
		/// <param name="result">Result to validate</param>
		/// <param name="message">Message to display when condition is not met</param>
		public static void ShouldPass(this ExecutionResult result, string message)
		{
			if (result == null)
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			if (result.Results.Any())
				if (string.IsNullOrEmpty(message))
					throw new AssertionFailedException(result);
				else
					throw new AssertionFailedException(message, result);
		}

		/// <summary>
		/// The result is expected to have errors.
		/// </summary>
		/// <param name="result">Result to validate</param>
		/// <param name="message">Message to display when condition is not met</param>
		public static void ShouldFail(this ExecutionResult result, string message)
		{
			if (result == null)
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);

			if (!result.Results.Any())
				if (string.IsNullOrEmpty(message))
					throw new AssertionFailedException(result);
				else
					throw new AssertionFailedException(message, result);
		}

		/// <summary>
		/// The result should contain at least one failure result for the specified rule.
		/// </summary>
		/// <typeparam name="T">Assertion rule type to look for</typeparam>
		/// <param name="result"></param>
		public static void ShouldFailForRule<T>(this ExecutionResult result) where T : AssertionRule
		{
			if (result == null)
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			result.ShouldFailForRule(typeof(T));
		}

		/// <summary>
		/// The result should contain at least one failure result for the specified rule.
		/// </summary>
		/// <param name="result"></param>
		/// <param name="type"></param>
		public static void ShouldFailForRule(this ExecutionResult result, Type type)
		{
			if (result == null)
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			if (type == null)
				throw new ArgumentNullException(nameof(type), ErrorMessages.NoTypeSpecifiedForAssertion);
			if (!result.Results.Any(r => r.CausedBy.GetType() == type))
				throw new AssertionFailedException(result);
		}

		internal static string GetString(this IEnumerable<AssertionResult> result)
		{
			if (result == null) return string.Empty;
			var builder = new StringBuilder();
			foreach (var r in result)
			{
				builder.AppendLine(r.GetString());
			}
			return builder.ToString();
		}

		internal static string GetString(this AssertionResult result)
		{
			return $"[{result.CausedBy.RuleName}] {result.ErrorMessage}";
		}
	}
}