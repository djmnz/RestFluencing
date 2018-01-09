using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using restfluencing.Tests.Models;
using restfluencing;
using restfluencing.Client;

namespace restfluencing.Tests.Clients
{
	public class TestApiClient : IApiClient
	{
		public Dictionary<string, object> Responses { get; }

		public TestApiClient(Dictionary<string, object> responses)
		{
			Responses = responses;
		}

		public void Dispose()
		{
		}

		public IApiClientResponse ExecuteRequest(IApiClientRequest request)
		{
			return new ApiClientResponse()
			{
				Content = JsonConvert.SerializeObject(Responses[request.Uri.AbsolutePath]),
				StatusCode = HttpStatusCode.OK
			};
		}
	}
}