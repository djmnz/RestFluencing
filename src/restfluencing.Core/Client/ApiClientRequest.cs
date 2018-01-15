using System;
using System.Collections.Generic;

namespace RestFluencing.Client
{
	public class ApiClientRequest : IApiClientRequest
	{
		public IDictionary<string, IList<string>> Headers { get; set; } = new Dictionary<string, IList<string>>();
		public Uri Uri { get; set; }
		public int TimeoutInSeconds { get; set; }
		public string Content { get; set; }
		public HttpVerb Verb { get; set; }
	}
}