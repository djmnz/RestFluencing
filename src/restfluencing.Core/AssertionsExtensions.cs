using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using restfluencing.Assertion;

namespace restfluencing
{
	public static class AssertionsExtensions
	{
		public static void ShouldPass(this ExecutionResult result)
		{
		    foreach (var r in result.Results)
		    {
		        Console.WriteLine(r.ErrorMessage);
		    }
            if (result.Results.Any())
			{
				throw new AssertionFailedException(result);
			}
		}

		public static void ShouldFail(this ExecutionResult result)
		{
		    foreach (var r in result.Results)
		    {
		        Console.WriteLine(r.ErrorMessage);
		    }
			if (!result.Results.Any())
			{
				throw new AssertionFailedException(result);
			}
		}
		public static void ShouldFailForRuleName(this ExecutionResult result, string ruleName)
		{
			if (!result.Results.Any(r => r.CausedBy.RuleName == ruleName))
			{
				throw new AssertionFailedException(result);
			}
		}

		public static void ShouldFailForRule<T>(this ExecutionResult result)
		{
			result.ShouldFailForRule(typeof(T));
		}
		public static void ShouldFailForRule(this ExecutionResult result, Type type)
		{
			if (!result.Results.Any(r => r.CausedBy.GetType() == type))
			{
				throw new AssertionFailedException(result);
			}
		}

		public static string GetString(this IEnumerable<AssertionResult> result)
		{
			if (result == null) return string.Empty;
			var builder = new StringBuilder();
			foreach (var r in result)
			{
				builder.AppendLine(r.GetString());
			}
			return builder.ToString();
		}

		public static string GetString(this AssertionResult result)
		{
			return $"[{result.CausedBy.RuleName}] {result.ErrorMessage}";
		}
	}
}