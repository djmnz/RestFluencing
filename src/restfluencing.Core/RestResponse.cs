using System;
using System.Collections.Generic;
using System.Linq;
using RestFluencing.Assertion;
using RestFluencing.Assertion.Rules;
using RestFluencing.Client;
using RestFluencing.Helpers;

namespace RestFluencing
{
	/// <summary>
	/// Response object that allows adding assertion rules so they can be validated against the original Request response.
	/// </summary>
	public class RestResponse
	{
		private readonly IList<AssertionRule> _rules = new List<AssertionRule>();

		/// <summary>
		///     Constructor used for sequential execution of the tests (specflow style)
		/// </summary>
		/// <param name="request">Request that originated this response.</param>
		/// <param name="context">Assertion context that this response will use</param>
		/// <param name="autoAssertWhenAddingRule">When <code>true</code> it will validate the rule as they are added. Default is <code>false</code></param>
		public RestResponse(RestRequest request, AssertionContext context, bool autoAssertWhenAddingRule)
		{
			Request = request ?? throw new ArgumentNullException(nameof(request), "No RestRequest has been provided.");
			Context = context ?? throw new ArgumentException(nameof(context), "No AssertionContext has been provided.");
			AutoAssertWhenAddingRule = autoAssertWhenAddingRule;
		}

		/// <summary>
		/// Context to be used when asserting the rules.
		/// </summary>
		protected AssertionContext Context { get; }

		/// <summary>
		///     Original Request that produced this response
		/// </summary>
		public RestRequest Request { get; }

		/// <summary>
		///     Whether the rules should be asserted as they are added - this elimiates the need to call Assert() at the end of the test.
		/// </summary>
		public bool AutoAssertWhenAddingRule { get; }

		/// <summary>
		///     List of rules that are attached to this response.
		/// </summary>
		public IEnumerable<AssertionRule> Rules => _rules;

        /// <summary>
        ///     Returns the Response
        /// </summary>
        public IApiClientResponse Response => Context.Response;

        /// <summary>
        ///     Adds a rule to be asserted. If <code>AutoAssertWhenAddingRule</code> is <code>true</code> then will also assert the new rule.
        /// </summary>
        /// <param name="rule"></param>
        public void AddRule(AssertionRule rule)
		{
			_rules.Add(rule);

			if (AutoAssertWhenAddingRule)
			{
				if (Request.Assertion == null)
					throw new InvalidOperationException(ErrorMessages.NoAssertion);

				var assertionResults = rule.Assert(Context);
				if (assertionResults.Any())
				{
					var result = CreateExecutionResult();
					result.Results.AddRange(assertionResults);
					Request.Assertion.Assert(result);
				}

			}
		}

		/// <summary>
		///     Removes all previous rules of same type and adds the new rule.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="rule"></param>
		public void OnlyOneRuleOf<T>(T rule) where T : AssertionRule
		{
			Array.ForEach(_rules.Where(r => r is T).ToArray(), r => _rules.Remove(r));
			AddRule(rule);
		}

		/// <summary>
		///     Gives the results of the assertion rules against the assertion context (response).
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		///     This is essential when using the DelayAssertion = true
		/// </remarks>
		public ExecutionResult Execute()
		{
			var result = CreateExecutionResult();

			foreach (var rule in Rules)
			{
				result.Results.AddRange(rule.Assert(Context));
			}

			return result;
		}


		/// <summary>
		///     This will assert all of the rules and expect that there will be no failure from their execution.
		/// </summary>
		/// <remarks>
		///     This is essential when using the DelayAssertion = true
		/// </remarks>
		public void Assert()
		{
			if (Request.Assertion == null)
				throw new InvalidOperationException(ErrorMessages.NoAssertion);

			// since the rules were not being asserted, then we assert them now.
			var executionResult = Execute();
			if (executionResult.Results.Any())
			{
				Request.Assertion.Assert(executionResult);
			}
		}

		/// <summary>
		///		This will execute all the rules and expect that it will have at least one failure from the assertion rules.
		/// </summary>
		public void AssertFailure()
		{
			if (Request.Assertion == null)
				throw new InvalidOperationException(ErrorMessages.NoAssertion);



			var executionResult = Execute();
			if (!executionResult.Results.Any())
			{
				executionResult.Results.Add(new AssertionResult(new AssertFailureRule(), "Expected at least one assertion failure, found none. All the assertion rules were met."));
				Request.Assertion.Assert(executionResult);
			}
		}

		private ExecutionResult CreateExecutionResult()
		{
			var result = new ExecutionResult();
			result.Response = Context.Response;

			var assertionResults = new List<AssertionResult>();
			result.Results = assertionResults;

			return result;
		}
	}
}