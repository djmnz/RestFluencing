using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Extensions to help configure the RestConfiguration to use the standard HttpApiClient factory.
	/// </summary>
	public static class HttpApiClientSetupExtensions
	{
		/// <summary>
		/// Uses the standard HttpApiClient wrapper as the web client
		/// </summary>
		/// <param name="config">Configuration to apply to</param>
		/// <returns></returns>
		public static RestConfiguration UsingWebApiClient(this RestConfiguration config)
		{
			config.ClientFactory = new HttpApiClientBuilder();
			return config;
		}


		/// <summary>
		/// Always uses the same http client.
		/// Important: you need to manage the lifetime of the client instance.
		/// </summary>
		/// <param name="config">Configuration to apply to</param>
		/// <param name="client">Client to be reused. You manage the lifetime of the instance</param>
		/// <returns></returns>
		public static RestConfiguration UsingWebApiClient(this RestConfiguration config, HttpClient client)
		{
			// TODO write tests
			config.ClientFactory = new ReUseHttpApiClientBuilder(client);
			return config;
		}
	}
}