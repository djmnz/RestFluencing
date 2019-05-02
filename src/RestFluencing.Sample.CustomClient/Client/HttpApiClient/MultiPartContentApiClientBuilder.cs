using System.Net.Http;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Standard http api client.
	/// </summary>
	public class MultiPartContentApiClientBuilder : IClientBuilder
	{
        private HttpClient _client;

        public MultiPartContentApiClientBuilder() : this(null)
        {
            
        }

        public MultiPartContentApiClientBuilder(HttpClient reuseHttpClient)
        {
            _client = reuseHttpClient;
        }

		/// <summary>
		/// Creates the standard http api client.
		/// </summary>
		/// <returns></returns>
		public IApiClient Create()
		{
			return new MultiPartContentApiClient(_client);
		}

		/// <summary>
		/// Creates an standard http api client request.
		/// </summary>
		/// <returns></returns>
		public IApiClientRequest CreateRequest()
		{
			return new MultipartFormDataRequest();
		}
	}
}