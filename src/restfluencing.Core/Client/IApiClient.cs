using System;

namespace RestFluencing.Client
{
	/// <summary>
	/// Base contract to create an api client.
	/// </summary>
	public interface IApiClient : IDisposable
	{
		/// <summary>
		/// Executes the request and returns a response.
		/// </summary>
		/// <param name="request">Request to send to the api.</param>
		/// <returns>Response of the api.</returns>
		IApiClientResponse ExecuteRequest(IApiClientRequest request);
	}
}