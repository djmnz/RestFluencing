using System.Net.Http;
using RestFluencing.Client;

namespace RestFluencing.Sample.CustomClient.Client
{
	/// <summary>
	/// EXAMPLE
	/// This is an example on how to create a generic api client builder for your custom client.
	/// </summary>
	public class MultipartContentApiClientBuilder : IClientBuilder
	{
        private HttpClient _client;

        public MultipartContentApiClientBuilder() : this(null)
        {
            
        }

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