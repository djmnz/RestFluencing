using System.Collections;
using System.Collections.Generic;

namespace restfluencing.Client
{
	public interface IApiClientResponse
	{
		int Status { get; set; }
		HttpStatusCode StatusCode { get; set; }
		IDictionary<string, IEnumerable<string>> Headers { get; set; }
		string Content { get; set; }
	}

	public class ApiClientResponse : IApiClientResponse
	{
		public int Status { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public IDictionary<string, IEnumerable<string>> Headers { get; set; }
		public string Content { get; set; }
	}
}