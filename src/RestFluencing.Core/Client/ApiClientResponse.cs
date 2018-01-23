using System.Collections.Generic;

namespace RestFluencing.Client
{
	/// <summary>
	/// Standard response of the API
	/// </summary>
	public class ApiClientResponse : IApiClientResponse
	{
		/// <summary>
		/// Status code returned in a number format.
		/// </summary>
		public int Status { get; set; }
		/// <summary>
		/// Enumeratation representation of the response status
		/// </summary>
		public HttpStatusCode StatusCode { get; set; }
		/// <summary>
		/// Headers of the response.
		/// </summary>
		public IDictionary<string, IEnumerable<string>> Headers { get; set; }
		/// <summary>
		/// Body content of the response.
		/// </summary>
		public string Content { get; set; }
	}
}