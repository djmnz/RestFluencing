using System;
using System.Collections.Generic;

namespace restfluencing.Client
{
	/// <summary>
	/// Representation of the request
	/// </summary>
	public interface IApiClientRequest
	{
		/// <summary>
		/// Headers that should be applied to the request
		/// </summary>
		IDictionary<string, IList<string>> Headers { get; set; }

		/// <summary>
		/// Uri to send the request to
		/// </summary>
		Uri Uri { get; set; }

		/// <summary>
		/// How long in seconds it should wait before timing out.
		/// </summary>
		int TimeoutInSeconds { get; set; }

		/// <summary>
		/// Content of the request.
		/// </summary>
		string Content { get; set; }

		/// <summary>
		/// HttpVerb to use in the request
		/// </summary>
		HttpVerb Verb { get; set; }
	}
}