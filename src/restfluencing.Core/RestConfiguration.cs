using System;
using System.Collections.Generic;
using RestFluencing.Assertion;
using RestFluencing.Client;
using RestFluencing.Client.HttpApiClient;
using RestFluencing.Helpers;

namespace RestFluencing
{
	/// <summary>
	///     Base configuration of the requests and clients
	/// </summary>
	/// <remarks>
	///		Most of the definition set in this instance are passed down into the Request rather than used from here.
	/// </remarks>
	public class RestConfiguration
	{
		/// <summary>
		///     Configuration of the client
		/// </summary>
		public IApiClientRequest RequestDefaults { get; set; } = new ApiClientRequest();

		/// <summary>
		///     HTTP client factory to be used when generating request
		/// </summary>
		public IClientBuilder ClientFactory { get; set; }

		/// <summary>
		///     Base URL to be used when creating the request URLs
		/// </summary>
		public Uri BaseUrl { get; private set; }

		/// <summary>
		///     Deserialiser to use when a response is received from the client
		/// </summary>
		public IResponseDeserialiser ResponseDeserialiser { get; set; } = null;

		/// <summary>
		///     How the assertion results should be handled.
		/// </summary>
		public IAssertion Assertion { get; set; } = new ExceptionAssertion();

		/// <summary>
		///     Overrides the base URL
		/// </summary>
		/// <param name="url">Uri to be the base URL</param>
		/// <returns></returns>
		public RestConfiguration WithBaseUrl(string url)
		{
			BaseUrl = new Uri(url);
			return this;
		}

		/// <summary>
		///     Overrides the base URL
		/// </summary>
		/// <param name="url">Uri to be the base URL</param>
		/// <returns></returns>
		public RestConfiguration WithBaseUrl(Uri url)
		{
			BaseUrl = url;
			return this;
		}

		/// <summary>
		///     Default configuration for asserting an Http Json end point.
		///		Sets the Accept and Content-Type to be application/json, uses the HttpClient and the JsonDeserialiser.
		/// </summary>
		/// <returns></returns>
		public static RestConfiguration JsonDefault()
		{
			var restDefaults = new RestConfiguration
			{
				RequestDefaults =
				{
					Headers = new Dictionary<string, IList<string>>
					{
						{"Accept", new List<string> {"application/json"}},
						{"Content-Type", new List<string> {"application/json"}}
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