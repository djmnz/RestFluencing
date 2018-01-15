using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestFluencing.Tests.Models;
using RestFluencing;
using RestFluencing.Client;

namespace RestFluencing.Tests.Clients
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
			if (Responses[request.Uri.AbsolutePath] == null)
			{
				return new ApiClientResponse()
				{
					Content = "",
					StatusCode = HttpStatusCode.OK
				};
			}
			return new ApiClientResponse()
			{
				Content = JsonConvert.SerializeObject(Responses[request.Uri.AbsolutePath]),
				StatusCode = HttpStatusCode.OK
			};
		}
	}
}