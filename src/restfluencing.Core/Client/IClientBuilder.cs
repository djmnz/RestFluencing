namespace RestFluencing.Client
{
	/// <summary>
	/// Factory interface to create an client to connect to apis.
	/// </summary>
	public interface IClientBuilder
	{
		/// <summary>
		/// Creates the API client.
		/// </summary>
		/// <returns></returns>
		IApiClient Create();


		/// <summary>
		/// Creates a request for the API client.
		/// </summary>
		/// <returns></returns>
		IApiClientRequest CreateRequest();
	}
}