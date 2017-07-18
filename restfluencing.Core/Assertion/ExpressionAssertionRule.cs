using System;
using System.Collections.Generic;
using restfluencing.Assertion.Rules;

namespace restfluencing.Assertion
{
	public class ExpressionAssertionRule<T> : AssertionRule
	{
		private const string _defaultError = @"Expression {0} is not found";
		private const string _expressionException = @"Expression {0} failed with exception {1}";
		private readonly Func<T, bool> _expression;
		private readonly string _error;
	    private readonly Type _assertType;
		private string _assertBody;

		public ExpressionAssertionRule(Func<T, bool> expression, string error = _defaultError) : base("Expression")
		{
			_expression = expression;
			_error = error;
		    _assertType = typeof(T);
			_assertBody = _expression.Method.GetMethodBody().ToString();
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			T obj = default(T);
			Exception error = null;
			try
			{
				obj = context.ResponseDeserialiser.GetResponse<T>(context);
			}
			catch (Exception ex)
			{
				error = ex;
			}

			if (obj == null || error != null)
			{
				yield return new AssertionResult(this, $"Failed to deserialise object. {_assertType}");
				yield break;
			}

			AssertionResult result = null;

			try
			{
				if (!_expression(obj))
				{
					
					result = new AssertionResult(this, string.Format(_error, _assertType));
				}
			}
			catch (Exception ex)
			{
				result = new AssertionResult(this, ex.ToString());
			}
			yield return result;

		}

	}
}