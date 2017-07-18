using System.Collections.Generic;

namespace restfluencing.Assertion.Rules
{
	public class HttpStatusRule : AssertionRule
	{
		public HttpStatusCode ExpectedStatusCode { get; }

		public HttpStatusRule(HttpStatusCode expectedStatusCode) : base("HttpStatus")
		{
			ExpectedStatusCode = expectedStatusCode;
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			if (context.Response.StatusCode != ExpectedStatusCode)
			{
				yield return new AssertionResult(this, $"Expected status code of {ExpectedStatusCode}, found {context.Response.StatusCode}");
			}
		}
	}
}