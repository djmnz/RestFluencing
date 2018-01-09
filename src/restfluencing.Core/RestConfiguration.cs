using System;
using System.Collections.Generic;
using restfluencing.Assertion;
using restfluencing.Client;
using restfluencing.Client.HttpApiClient;
using restfluencing.Helpers;

namespace restfluencing
{
	/// <summary>
	/// Base configuration of the requests and clients
	/// </summary>
	public class RestConfiguration
	{
		/// <summary>
		/// Configuration of the client
		/// </summary>
		public IApiClientRequest RequestDefaults { get; set; } = new ApiClientRequest();

		/// <summary>
		/// HTTP client factory to be used when generating request
		/// </summary>
		public IClientBuilder ClientFactory { get; set; }

		/// <summary>
		/// Base URL to be used when creating the request URLs
		/// </summary>
		public Uri BaseUrl { get; private set; } = null;

		/// <summary>
		/// Deserialiser to use when a response is received from the client
		/// </summary>
		public IResponseDeserialiser ResponseDeserialiser { get; set; } = null;

		public IAssertion Assertion { get; set; } = new ExceptionAssertion();

		public RestConfiguration WithBaseUrl(string url)
		{
			BaseUrl = new Uri(url);
			return this;
		}

		public RestConfiguration WithBaseUrl(Uri url)
		{
			BaseUrl = url;
			return this;
		}



		public static RestConfiguration JsonDefault()
		{
			var restDefaults = new RestConfiguration
			{

				RequestDefaults =
				{
					Headers = new Dictionary<string, IList<string>>()
					{
						{"Accept", new List<string> { "application/json" } },
						{"Content-Type", new List<string> { "application/json" } }
					},
					TimeoutInSeconds = DefaultValues.TimeOutInSeconds
				}

			};

			//TODO this may need to move out if we want to make the httpclient library an optional dependency to help with other .net implementations (i.e. core, phone, etc)
			restDefaults.UsingWebApiClient();

			restDefaults.UseJsonResponseDeserialiser();


			return restDefaults;
		}
	}
}