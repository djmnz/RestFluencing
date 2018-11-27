using System;
using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Inline validation rule
	/// </summary>
	public class InlineAssertionRule<T> : AssertionRule
	{
		private const string _expressionError = "Failed to assert because an exception occurred {0}";
		private readonly Func<T, bool> _lambda;
		private readonly Func<T, string> _errorMessage;
		private readonly string _error;
		private readonly Type _assertType;

		/// <summary>
		/// Constructor with a static error message
		/// </summary>
		/// <param name="lambda"></param>
		/// <param name="error"></param>
		public InlineAssertionRule(Func<T, bool> lambda, string error) : base("Inline")
		{
			_assertType = typeof(T);
			_lambda = lambda;
			_error = error ?? throw new ArgumentNullException(nameof(error), "An error is required.");
		}

		/// <summary>
		/// Constructor with a static error message
		/// </summary>
		/// <param name="lambda"></param>
		/// <param name="errorMessage">Allows a formatted error message with information about the model.</param>
		public InlineAssertionRule(Func<T, bool> lambda, Func<T, string> errorMessage) : base("Inline")
		{
			_assertType = typeof(T);
			_lambda = lambda;
			_errorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage), "An error is required."); ;
		}



		/// <summary>
		/// Asserts that the function returns true.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (string.IsNullOrEmpty(context.Response.Content))
			{
				yield return new AssertionResult(this, $"Response was blank and could not assert as {_assertType}.");
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
				yield return new AssertionResult(this, string.Format(_expressionError, error));

			if (obj == null || error != null)
				yield break;

			AssertionResult result = null;

			try
			{
				if (!_lambda(obj))
				{
					string errorMessage = _error;
					
					// if we have a function to generate the error we use that instead
					if (_errorMessage != null)
					{
						// now we need to be safe - an error here doesn't mean that the whole thing should not necessarily crash 
						try
						{
							errorMessage = _errorMessage(obj);
						}
						catch (Exception ex)
						{
							errorMessage =
								$"Assertion failed and could not generate the message due to an exception on the custom error message function {ex.Message}";
						}
					}

					result = new AssertionResult(this, errorMessage);
				}
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