namespace restfluencing.Client
{
	public interface IClientBuilder
	{
		IApiClient Create();

		IApiClientRequest CreateRequest();
	}
}