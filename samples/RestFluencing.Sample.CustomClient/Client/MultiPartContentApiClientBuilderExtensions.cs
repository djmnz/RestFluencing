using System.Net.Http;

namespace RestFluencing.Sample.CustomClient.Client
{
	/// <summary>
	/// Extensions to help configure the RestConfiguration to use the standard HttpApiClient factory.
	/// </summary>
	public static class MultipartContentApiClientBuilderExtensions
    {
		/// <summary>
		/// Uses the standard HttpApiClient wrapper as the web client
		/// </summary>
		/// <param name="config">Configuration to apply to</param>
		/// <returns></returns>
		public static RestConfiguration UsingMultipartApiClient(this RestConfiguration config)
		{
			config.ClientFactory = new MultipartContentApiClientBuilder();
			return config;
		}


		/// <summary>
		/// Always uses the same http client.
		/// Important: you need to manage the lifetime of the client instance.
		/// </summary>
		/// <param name="config">Configuration to apply to</param>
		/// <param name="client">Client to be reused. You manage the lifetime of the instance</param>
		/// <returns></returns>
		public static RestConfiguration UsingMultipartApiClient(this RestConfiguration config, HttpClient client)
		{
			config.ClientFactory = new MultipartContentApiClientBuilder(client);
			return config;
		}
	}
}