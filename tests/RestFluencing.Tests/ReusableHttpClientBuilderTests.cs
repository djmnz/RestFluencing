using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Client.HttpApiClient;

namespace RestFluencing.Tests
{
	[TestClass]
	public class ReusableHttpClientBuilderTests
	{

		[TestMethod]
		public void WhenHasClientShouldSetTheReusableHttpApiClientFactory()
		{
			var config = new RestConfiguration();
			var client = new HttpClient();

			config.UsingWebApiClient(client);

			Assert.IsInstanceOfType(config.ClientFactory, typeof(ReUseHttpApiClientBuilder));
		}

		[TestMethod]
		public void ReusableBuilderCreatesReusableClient()
		{
			var httpClient = new HttpClient();

			var builder = new ReUseHttpApiClientBuilder(httpClient);

			var client = builder.Create();

			Assert.IsInstanceOfType(client, typeof(ReUseHttpApiClient));
		}
	}
}