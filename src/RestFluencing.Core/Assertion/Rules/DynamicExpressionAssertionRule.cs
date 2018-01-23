using System;
using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Rule to validate a function delegate based on a dynamic object since that expressions cannot have dynamic types.
	/// </summary>
	public class DynamicExpressionAssertionRule : AssertionRule
	{
		private const string _defaultError = @"Expression {0} is not found";
		private readonly string _error;
		private readonly Func<dynamic, bool> _expression;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="error"></param>
		public DynamicExpressionAssertionRule(Func<dynamic, bool> expression, string error) : base("DynamicExpression")
		{
			_expression = expression;
			_error = error;
		}

		/// <summary>
		/// Asserts that the function returns true.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			dynamic obj = null;
			Exception error = null;
			try
			{
				obj = context.ResponseDeserialiser.GetResponse<dynamic>(context);
			}
			catch (Exception ex)
			{
				error = ex;
			}

			if (obj == null || error != null)
			{
				yield return new AssertionResult(this, $"Failed to deserialise object.");
				yield break;
			}

			AssertionResult result = null;

			try
			{
				if (!_expression(obj))
					result = new AssertionResult(this, _error);
			}
			catch (Exception ex)
			{
				result = new AssertionResult(this, ex.ToString());
			}

			if (result != null)
				yield return result;
		}
	}
}