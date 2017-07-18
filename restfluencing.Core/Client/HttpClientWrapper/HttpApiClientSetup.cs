namespace restfluencing.Client.HttpApiClient
{
	public static class HttpApiClientSetup
	{
		public static RestDefaults UsingWebApi(this RestDefaults request)
		{
			request.ClientFactory = new HttpApiClientBuilder();
			return request;
		}
		public static RestDefaults UseJsonResponseDeserialiser(this RestDefaults request)
		{
			request.ResponseDeserialiser = new JsonResponseDeserialiser();
			return request;
		}
	}
}