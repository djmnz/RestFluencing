using System.Net.Http;

namespace RestFluencing.Client.MultipartApiClient
{
	/// <summary>
	/// Standard http api client.
	/// </summary>
	public class MultipartContentApiClientBuilder : IClientBuilder
	{
        private readonly HttpClient _client;

		/// <summary>
		/// Builder with no reuse of client.
		/// </summary>
        public MultipartContentApiClientBuilder() : this(null)
        {
            
        }

		/// <summary>
		/// Builder with a client to reuse.
		/// </summary>
		/// <param name="reuseHttpClient"></param>
        public MultipartContentApiClientBuilder(HttpClient reuseHttpClient)
        {
            _client = reuseHttpClient;
        }

		/// <summary>
		/// Creates the standard http api client.
		/// </summary>
		/// <returns></returns>
		public IApiClient Create()
		{
			return new MultipartContentApiClient(_client);
		}

		/// <summary>
		/// Creates an standard http api client request.
		/// </summary>
		/// <returns></returns>
		public IApiClientRequest CreateRequest()
		{
			return new MultipartFormClientRequest();
		}
	}
}