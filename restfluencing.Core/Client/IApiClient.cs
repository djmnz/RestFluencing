using System;

namespace restfluencing.Client
{
	public interface IApiClient : IDisposable
	{
		IApiClientResponse ExecuteRequest(IApiClientRequest request);
	}
}