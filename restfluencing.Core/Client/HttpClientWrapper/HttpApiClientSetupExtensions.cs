namespace restfluencing.Client.HttpApiClient
{
	public static class HttpApiClientSetupExtensions
	{
		public static RestConfiguration UsingWebApi(this RestConfiguration request)
		{
			request.ClientFactory = new HttpApiClientBuilder();
			return request;
		}
		public static RestConfiguration UseJsonResponseDeserialiser(this RestConfiguration request)
		{
			request.ResponseDeserialiser = new JsonResponseDeserialiser();
			return request;
		}
	}
}