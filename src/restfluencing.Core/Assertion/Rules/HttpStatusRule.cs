using System.Collections.Generic;

namespace RestFluencing.Assertion.Rules
{
	/// <summary>
	/// Rule to validate that the response returns the specified status code.
	/// </summary>
	public class HttpStatusRule : AssertionRule
	{
		private readonly HttpStatusCode _expectedStatusCode;


		/// <summary>
		/// 
		/// </summary>
		/// <param name="expectedStatusCode"></param>
		public HttpStatusRule(HttpStatusCode expectedStatusCode) : base("HttpStatus")
		{
			_expectedStatusCode = expectedStatusCode;
		}

		/// <summary>
		/// Asserts that it contains the specified status
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (context.Response.StatusCode != _expectedStatusCode)
			{
				yield return new AssertionResult(this, $"Expected status code of {_expectedStatusCode}, found {context.Response.StatusCode}");
			}
		}
	}
}