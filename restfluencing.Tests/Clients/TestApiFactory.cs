using System.Collections.Generic;
using restfluencing.Client;

namespace restfluencing.Tests.Clients
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