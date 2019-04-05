using System.Net.Http;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Api Client that reuses the same HttpClient for every request.
	/// </summary>
	/// <remarks>
	/// RestFluencing will not dispose the client.
	/// </remarks>
	public class ReUseHttpApiClient : HttpApiClientBase
	{

		/// <summary>
		/// Specifies the client to use
		/// </summary>
		/// <param name="client"></param>
		public ReUseHttpApiClient(HttpClient client)
		{
			HttpClient = client;
		}

		/// <summary>
		/// The client being used
		/// </summary>
		protected HttpClient HttpClient { get; set; }

		/// <inheritdoc />
		protected override HttpClient CreateClient()
		{
			return HttpClient;
		}

		/// <inheritdoc />
		protected override void DisposeClient()
		{
			// do nothing, we don't want to Dispose
		}
	}
}