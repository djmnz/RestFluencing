using System.Net.Http;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Standard http client that creates a new instance every request.
	/// </summary>
	public class HttpApiClient : HttpApiClientBase
	{
		/// <summary>
		/// The client being used
		/// </summary>
		protected HttpClient HttpClient { get; set; }

		/// <inheritdoc />
		protected override HttpClient CreateClient()
		{
			HttpClient = new HttpClient();
			return HttpClient;
		}

		/// <inheritdoc />
		protected override void DisposeClient()
		{
			if (HttpClient != null)
			{
				HttpClient.Dispose();
				HttpClient = null;
			}
		}
	}
}