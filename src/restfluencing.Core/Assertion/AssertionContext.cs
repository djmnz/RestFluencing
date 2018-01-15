using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using RestFluencing.Client;

namespace RestFluencing.Assertion
{
	/// <summary>
	/// Assertion context to process the assertion rules.
	/// </summary>
	public class AssertionContext
	{
		private readonly IList<AssertionResult> _results = new List<AssertionResult>();
		/// <summary>
		/// Request used to generate the Response
		/// </summary>
		public IApiClientRequest Request { get; set; }

		/// <summary>
		/// Client used to send the Request
		/// </summary>
		public IApiClient Client { get; set; }

		/// <summary>
		/// Response returned for the Request submitted
		/// </summary>
		public IApiClientResponse Response { get; set; }

		/// <summary>
		/// Deserialiser to use for deserialising the Response body
		/// </summary>
		public IResponseDeserialiser ResponseDeserialiser { get; set; }

		/// <summary>
		/// Shared properties in the context
		/// </summary>
		public IDictionary<string, object> Properties { get; set; } = new ConcurrentDictionary<string, object>();

	}
}