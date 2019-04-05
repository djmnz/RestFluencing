using System.Net.Http;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Reusuable HttpClient builder.
	/// </summary>
	public class ReUseHttpApiClientBuilder : IClientBuilder
	{
		private readonly HttpClient _client;

		/// <summary>
		/// Specifies the client to use
		/// </summary>
		/// <param name="client"></param>
		public ReUseHttpApiClientBuilder(HttpClient client)
		{
			_client = client;
		}

		/// <summary>
		/// Creates the standard http api client.
		/// </summary>
		/// <returns></returns>
		public IApiClient Create()
		{
			return new ReUseHttpApiClient(_client);
		}

		/// <summary>
		/// Creates an standard http api client request.
		/// </summary>
		/// <returns></returns>
		public IApiClientRequest CreateRequest()
		{
			return new ApiClientRequest();
		}
	}
}