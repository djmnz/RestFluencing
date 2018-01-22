using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestFluencing.Assertion;
using RestFluencing.Helpers;

namespace RestFluencing
{
	public static class AssertionsExtensions
	{
		public static void ShouldPass(this RestResponse response)
		{
			if (response == null)
			{
				throw new ArgumentNullException(nameof(response), ErrorMessages.NoResponse);
			}
			response.Assert();
		}

		public static void ShouldPass(this ExecutionResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
			result.ShouldPass(null);
		}

		public static void ShouldFail(this ExecutionResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
			result.ShouldFail(null);
		}


		public static void ShouldPass(this ExecutionResult result, string message)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
            if (result.Results.Any())
			{
				if (string.IsNullOrEmpty(message))
				{
					throw new AssertionFailedException(result);
				}
				else
				{
					throw new AssertionFailedException(message, result);
				}
			}
		}

		public static void ShouldFail(this ExecutionResult result, string message)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
		    foreach (var r in result.Results)
		    {
		        Console.WriteLine(r.ErrorMessage);
		    }
			if (!result.Results.Any())
			{
				if (string.IsNullOrEmpty(message))
				{
					throw new AssertionFailedException(result);
				}
				else
				{
					throw new AssertionFailedException(message, result);
				}
			}
		}
		public static void ShouldFailForRuleName(this ExecutionResult result, string ruleName)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
			if (!result.Results.Any(r => r.CausedBy.RuleName == ruleName))
			{
				throw new AssertionFailedException(result);
			}
		}

		public static void ShouldFailForRule<T>(this ExecutionResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
			result.ShouldFailForRule(typeof(T));
		}
		public static void ShouldFailForRule(this ExecutionResult result, Type type)
		{
			if (result == null)
			{
				throw new ArgumentNullException(nameof(result), ErrorMessages.NoResult);
			}
			if (type == null)
			{
				throw new ArgumentNullException(nameof(type), ErrorMessages.NoTypeSpecifiedForAssertion);
			}
			if (!result.Results.Any(r => r.CausedBy.GetType() == type))
			{
				throw new AssertionFailedException(result);
			}
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