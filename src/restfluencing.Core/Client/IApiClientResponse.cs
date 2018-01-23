using System.Collections.Generic;

namespace RestFluencing.Client
{
	/// <summary>
	///     Definition of an api response
	/// </summary>
	public interface IApiClientResponse
	{
		/// <summary>
		///     Integer representation of the status code returned by the api.
		/// </summary>
		int Status { get; set; }

		/// <summary>
		///     Enumeration of the status code.
		/// </summary>
		HttpStatusCode StatusCode { get; set; }

		/// <summary>
		///     Headers of the response.
		/// </summary>
		IDictionary<string, IEnumerable<string>> Headers { get; set; }

		/// <summary>
		///     Body content of the response.
		/// </summary>
		string Content { get; set; }
	}
}