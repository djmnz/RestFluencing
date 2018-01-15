using System.Collections.Generic;
using RestFluencing.Client;

namespace RestFluencing.Tests.Clients
{
	public class TestApiFactory : IClientBuilder
	{
		public Dictionary<string, object> Responses { get; set; } = new Dictionary<string, object>();

		public IApiClient Create()
		{
			return new TestApiClient(Responses);
		}

		public IApiClientRequest CreateRequest()
		{
			return new ApiClientRequest();
		}
	}
}