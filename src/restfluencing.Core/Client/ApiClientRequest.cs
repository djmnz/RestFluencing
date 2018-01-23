using System;
using System.Collections.Generic;

namespace RestFluencing.Client
{
	/// <summary>
	///     Represents the request that will be submitted
	/// </summary>
	public class ApiClientRequest : IApiClientRequest
	{
		/// <summary>
		///     Headers to submit
		/// </summary>
		public IDictionary<string, IList<string>> Headers { get; set; } = new Dictionary<string, IList<string>>();

		/// <summary>
		///     URI to send the request to.
		/// </summary>
		public Uri Uri { get; set; }

		/// <summary>
		///     How long it shouild wait for a timeout
		/// </summary>
		public int TimeoutInSeconds { get; set; }

		/// <summary>
		///     Content in the body.
		/// </summary>
		public string Content { get; set; }

		/// <summary>
		///     Verb to use
		/// </summary>
		public HttpVerb Verb { get; set; }
	}
}