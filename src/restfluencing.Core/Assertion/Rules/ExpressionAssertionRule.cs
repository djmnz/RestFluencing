using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	///     Standard assertion rule that uses the Deserialiser to compare the expression to the respone.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ExpressionAssertionRule<T> : AssertionRule
	{
		private const string _defaultError = @"Response did not return {0}";
		private const string _expressionException = @"Expression {0} failed with exception {1}";
		private readonly Type _assertType;
		private readonly string _error;
		private readonly Expression<Func<T, bool>> _expression;
		private readonly Func<T, bool> _func;
		private readonly string _assertBody;

		/// <summary>
		///     Minimum requirements for the expression assertion rule
		/// </summary>
		/// <param name="expression"></param>
		/// <param name="error">Error message to display when assertion fails.
		/// {0} is the expression body.</param>
		public ExpressionAssertionRule(Expression<Func<T, bool>> expression, string error = _defaultError) :
			base("Expression")
		{
			_expression = expression;
			_func = _expression.Compile();
			_error = error;
			_assertType = typeof(T);
			_assertBody = _expression.Body.ToString();
		}

		/// <summary>
		/// Assets that the expression is true.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (string.IsNullOrEmpty(context.Response.Content))
			{
				yield return new AssertionResult(this, $"Response was blank and could not assert {_assertBody}");
				yield break;
			}

			var obj = default(T);
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
				yield return new AssertionResult(this, $"Failed to deserialise response to {_assertType}");

			if (error != null)
				yield return new AssertionResult(this, string.Format(_expressionException, _assertBody, error));

			if (obj == null || error != null)
				yield break;

			AssertionResult result = null;

			try
			{
				if (!_func(obj))
					result = new AssertionResult(this, string.Format(_error, _assertBody));
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