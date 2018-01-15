namespace RestFluencing.Client
{
	public interface IClientBuilder
	{
		IApiClient Create();

		IApiClientRequest CreateRequest();
	}
}