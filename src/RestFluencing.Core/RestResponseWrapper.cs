using System;
using RestFluencing.Assertion.Rules;

namespace RestFluencing
{
	/// <summary>
	/// This is a helper class to facilitate creating specialised chained assertion over a response.
	/// </summary>
	public class RestResponseWrapper : IRestResponse
	{
		private readonly IRestResponse _response;

		public RestResponseWrapper(IRestResponse response)
		{
			_response = response ?? throw new ArgumentNullException(nameof(response), "No Response provided to wrap.");
		}

		public void AddRule(AssertionRule rule)
		{
			_response.AddRule(rule);
		}

		public void OnlyOneRuleOf<T>(T rule) where T : AssertionRule
		{
			_response.OnlyOneRuleOf(rule);
		}

		public ExecutionResult Execute()
		{
			return _response.Execute();
		}

		public void Assert()
		{
			_response.Assert();
		}

		public void AssertFailure()
		{
			_response.AssertFailure();
		}
	}

	/// <summary>
	/// The rest response assuming model of specific type.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>
	/// This class is not suppose to do anything, it should be only a wrapper to allow some syntax sugar.
	/// </remarks>
	public class RestResponseOfModel<T> : RestResponseWrapper
	{
		public RestResponseOfModel(IRestResponse response) : base(response)
		{
			
		}
	}
}