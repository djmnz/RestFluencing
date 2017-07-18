namespace restfluencing.Client.HttpApiClient
{
	public class HttpApiClientBuilder : IClientBuilder
	{
		public IApiClient Create()
		{
			return new HttpApiClient();
		}

		public IApiClientRequest CreateRequest()
		{
			return new ApiClientRequest();
		}
	}
}