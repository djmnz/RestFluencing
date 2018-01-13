using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace restfluencing.Assertion.Rules
{
	/// <summary>
	/// Standard assertion rule that uses the Deserialiser to compare the expression to the respone.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ExpressionAssertionRule<T> : AssertionRule
	{
		private const string _defaultError = @"Response did not return {0}";
		private const string _expressionException = @"Expression {0} failed with exception {1}";
		private readonly Expression<Func<T, bool>> _expression;
		private readonly Func<T, bool> _func;
		private readonly string _error;
	    private readonly Type _assertType;
		private string _assertBody;

		public ExpressionAssertionRule(Expression<Func<T, bool>> expression, string error = _defaultError) : base("Expression")
		{
			_expression = expression;
			_func = _expression.Compile();
			_error = error;
		    _assertType = typeof(T);
			_assertBody = ((LambdaExpression)_expression).Body.ToString();
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{

			if (string.IsNullOrEmpty(context.Response.Content))
			{
				yield return new AssertionResult(this, $"Response was blank and could not assert {_assertBody}");
				yield break;

			}

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

			if (obj == null)
			{
				yield return new AssertionResult(this, $"Failed to deserialise response to {_assertType}");
			}

			if (error != null)
			{
				yield return new AssertionResult(this, string.Format(_expressionException, _assertBody, error));
			}

			if (obj == null || error != null)
			{
				yield break;
			}

			AssertionResult result = null;

			try
			{
				if (!_func(obj))
				{
					
					result = new AssertionResult(this, string.Format(_error, _assertBody));
				}
			}
			catch (Exception ex)
			{
				result = new AssertionResult(this, ex.ToString());
			}

			if (result != null)
			{
				yield return result;
			}
			yield break;

		}

	}

	public class DynamicExpressionAssertionRule : AssertionRule
	{
		private const string _defaultError = @"Expression {0} is not found";
		private readonly Func<dynamic, bool> _expression;
		private readonly string _error;
		
		public DynamicExpressionAssertionRule(Func<dynamic, bool> expression, string error) : base("DynamicExpression")
		{
			_expression = expression;
			_error = error;
		}

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
				{

					result = new AssertionResult(this, _error);
				}
			}
			catch (Exception ex)
			{
				result = new AssertionResult(this, ex.ToString());
			}

			if (result != null)
			{
				yield return result;
			}
			yield break;

		}

	}

}