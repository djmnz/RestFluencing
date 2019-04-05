using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Client.HttpApiClient;

namespace RestFluencing.Tests
{
	[TestClass]
	public class HttpApiClientBuilderTests
	{
		[TestMethod]
		public void WhenNoClientShouldSetTheHttpApiClientBuilder()
		{
			var config = new RestConfiguration();
			config.UsingWebApiClient();

			Assert.IsInstanceOfType(config.ClientFactory, typeof(HttpApiClientBuilder));
		}


		[TestMethod]
		public void HttpClientBuilderCreatesHttpApiClient()
		{
			var builder = new HttpApiClientBuilder();

			var client = builder.Create();

			Assert.IsInstanceOfType(client, typeof(HttpApiClient));
		}


	}
}