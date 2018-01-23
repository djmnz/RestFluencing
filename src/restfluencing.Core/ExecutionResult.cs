using System.Collections.Generic;
using RestFluencing.Client;
using RestFluencing.Assertion;
using System;
using System.Linq;

namespace RestFluencing
{
	/// <summary>
	/// Results of processing the Response's rules.
	/// </summary>
	public class ExecutionResult
	{
		/// <summary>
		/// List of assertion results that this instance has.
		/// </summary>
		public List<AssertionResult> Results { get; set; }

		/// <summary>
		/// Response that generate the results.
		/// </summary>
		public IApiClientResponse Response { get; set; }

		/// <summary>
		/// Throws an exception if it has any assertion results.
		/// </summary>
		public void Assert()
		{
			if (Results.Any())
			{
				throw new AssertionFailedException(this);
			}
		}
	}
}