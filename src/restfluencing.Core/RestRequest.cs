using System;
using System.Collections.Generic;
using restfluencing.Assertion;
using restfluencing.Client;
using restfluencing.Helpers;

namespace restfluencing
{
	/// <summary>
	///     Representation of the request setup
	/// </summary>
	public class RestRequest
	{
		public RestRequest(RestConfiguration configuration)
		{
			if (configuration.ClientFactory == null)
				throw new InvalidOperationException(ErrorMessages.NoClientFactory);

			Configuration = configuration;
			Request = configuration.ClientFactory.CreateRequest();
			Assertion = configuration.Assertion;
		}

		/// <summary>
		///     Request to be submitted to the api client.
		/// </summary>
		public IApiClientRequest Request { get; set; }

		/// <summary>
		///     Configuration that this request is relying on.
		/// </summary>
		public RestConfiguration Configuration { get; set; }

		/// <summary>
		///     Response deserialiser that this request will utilise
		/// </summary>
		public IResponseDeserialiser ResponseDeserialiser { get; set; }

		/// <summary>
		///     How the assertion results should be handled.
		/// </summary>
		public IAssertion Assertion { get; set; }

		/// <summary>
		///     Creates the request based on the request that we have been manipulating with the extensions
		///     and set the defaults where appropriate.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		private IApiClientRequest BuildRequest(IClientBuilder builder)
		{
			var result = builder.CreateRequest();

			// timeout
			result.TimeoutInSeconds = Request.TimeoutInSeconds;
			if (result.TimeoutInSeconds == 0)
				result.TimeoutInSeconds = Configuration.RequestDefaults.TimeoutInSeconds;

			// headers
			var headers = Request.Headers;
			var defaultHeaders = Configuration.RequestDefaults.Headers;
			if (headers == null)
				headers = defaultHeaders;
			else
				foreach (var k in defaultHeaders.Keys)
				{
					foreach (var v in defaultHeaders[k])
					{
						if (headers.ContainsKey(k))
						{
							if (!headers[k].Contains(v))
							{
								var list = headers[k];
								if (list == null)
									throw new InvalidOperationException(
										ErrorMessages.InvalidHeaderValueType);
								list.Add(v);
							}
							// If the value already exists then we dont try to add again as it is the default headers
						}
						else
						{
							headers.Add(k, new List<string> {v});
						}
					}
				}
			result.Headers = headers;

			// content
			result.Content = Request.Content;
			if (result.Content == null)
				result.Content = Configuration.RequestDefaults.Content;

			// verb
			result.Verb = Request.Verb;
			if (result.Verb == HttpVerb.Unknown)
				result.Verb = Configuration.RequestDefaults.Verb;


			// uri
			result.Uri = Request.Uri;
			if (result.Uri == null)
				result.Uri = Configuration.RequestDefaults.Uri;

			return result;
		}

		/// <summary>
		///     Process the request and returns the response to be asserted.
		/// </summary>
		/// <param name="delayAssertion"></param>
		/// <returns></returns>
		public RestResponse Response(bool delayAssertion = false)
		{
			var clientBuilder = Configuration.ClientFactory;
			if (clientBuilder == null)
				throw new InvalidOperationException(ErrorMessages.NoClientFactory);

			var responseDeserialiser = ResponseDeserialiser ?? Configuration.ResponseDeserialiser;
			if (responseDeserialiser == null)
				throw new InvalidOperationException(ErrorMessages.NoResponseDeserialiser);

			using (var client = clientBuilder.Create())
			{
				var context = new AssertionContext
				{
					Request = BuildRequest(clientBuilder),
					Client = client,
					ResponseDeserialiser = responseDeserialiser
				};
				context.Response = client.ExecuteRequest(context.Request);

				var response = new RestResponse(this, context, delayAssertion);
				return response;
			}
		}
	}
}