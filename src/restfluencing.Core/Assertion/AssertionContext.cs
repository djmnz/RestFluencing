using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using restfluencing.Client;

namespace restfluencing.Assertion
{
	public class AssertionContext
	{
		private readonly IList<AssertionResult> _results = new List<AssertionResult>();
		public IApiClientRequest Request { get; set; }
		public IApiClient Client { get; set; }
		public IApiClientResponse Response { get; set; }
		public IResponseDeserialiser ResponseDeserialiser { get; set; }

		public IEnumerable<AssertionResult> CurrentResults => _results;

		public IDictionary<string, object> Properties { get; set; } = new ConcurrentDictionary<string, object>();

		public void AddResult(AssertionResult result)
		{
			if (result == null)
			{
				throw new ArgumentNullException("result", "Cannot add a null result.");
			}
			_results.Add(result);
		}
	
	}
}