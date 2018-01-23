namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Standard http api client.
	/// </summary>
	public class HttpApiClientBuilder : IClientBuilder
	{
		/// <summary>
		/// Creates the standard http api client.
		/// </summary>
		/// <returns></returns>
		public IApiClient Create()
		{
			return new HttpApiClient();
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