using System.Collections.Generic;
using restfluencing.Client;
using restfluencing.Assertion;
using System;
using System.Linq;

namespace restfluencing
{
	public class ExecutionResult
	{
		public IEnumerable<AssertionResult> Results { get; set; }
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