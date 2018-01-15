using System;

namespace RestFluencing.Client
{
	public interface IApiClient : IDisposable
	{
		IApiClientResponse ExecuteRequest(IApiClientRequest request);
	}
}