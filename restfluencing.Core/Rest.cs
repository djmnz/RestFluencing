using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using restfluencing.Assertion;
using restfluencing.Client;
using restfluencing.Client.HttpApiClient;
using restfluencing.Helpers;

namespace restfluencing
{
	public static class Rest
	{
		public static RestDefaults Defaults { get; set; } = RestDefaults.JsonDefault();

		public static RestRequest GetFromUrl(Uri url, RestDefaults defaults = null)
		{
			return SendToUrl(HttpVerb.Get, url, defaults);
		}

		public static RestRequest PostToUrl(Uri url, RestDefaults defaults = null)
		{
			return SendToUrl(HttpVerb.Post, url, defaults);
		}

		public static RestRequest SendToUrl(HttpVerb verb, Uri url, RestDefaults defaults = null)
		{
			if (defaults == null)
			{
				defaults = Defaults;
			}

			var request = new RestRequest(defaults)
			{
				Request =
				{
					Uri = url,
					Verb = verb
				}
			};

			return request;
		}

		public static RestRequest GetFromUrl(string url, RestDefaults defaults = null)
		{
			return GetFromUrl(new Uri(url), defaults);
		}


		public static RestRequest Get(string relative, RestDefaults defaults = null)
		{
			if (defaults == null)
			{
				defaults = Defaults;
			}

			if (defaults.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return GetFromUrl(new Uri(defaults.BaseUrl, relative), defaults);
		}


		public static RestRequest Post(string relative, RestDefaults defaults = null)
		{
			if (defaults == null)
			{
				defaults = Defaults;
			}

			if (defaults.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return PostToUrl(new Uri(defaults.BaseUrl, relative), defaults);
		}


		public static RestRequest Put(string relative, RestDefaults defaults = null)
		{
			if (defaults == null)
			{
				defaults = Defaults;
			}

			if (defaults.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return SendToUrl(HttpVerb.Put, new Uri(defaults.BaseUrl, relative), defaults);
		}

		public static RestRequest Patch(string relative, RestDefaults defaults = null)
		{
			if (defaults == null)
			{
				defaults = Defaults;
			}

			if (defaults.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return SendToUrl(HttpVerb.Patch, new Uri(defaults.BaseUrl, relative), defaults);
		}

		public static RestRequest Delete(string relative, RestDefaults defaults = null)
		{
			if (defaults == null)
			{
				defaults = Defaults;
			}

			if (defaults.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return SendToUrl(HttpVerb.Delete, new Uri(defaults.BaseUrl, relative), defaults);
		}

		public static RestDefaults WithBaseUrl(string url)
		{
			Defaults.WithBaseUrl(url);
			return Defaults;
		}

		public static RestDefaults WithBaseUrl(Uri url)
		{
			Defaults.WithBaseUrl(url);
			return Defaults;
		}

	}

	public class RestDefaults
	{
		public IApiClientRequest RequestDefaults { get; set; } = new ApiClientRequest();

		public IClientBuilder ClientFactory { get; set; }


		public Uri BaseUrl { get; private set; } = null;
		public IResponseDeserialiser ResponseDeserialiser { get; set; } = null;

		public RestDefaults WithBaseUrl(string url)
		{
			BaseUrl = new Uri(url);
			return this;
		}

		public RestDefaults WithBaseUrl(Uri url)
		{
			BaseUrl = url;
			return this;
		}

		public static RestDefaults JsonDefault()
		{
			var restDefaults = new RestDefaults
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
			restDefaults.UsingWebApi();

			restDefaults.UseJsonResponseDeserialiser();


			return restDefaults;
		}
	}

	public class RestRequest
	{
		private Action<AssertionBuilder> assertions;

		public RestRequest(RestDefaults defaults)
		{
			if (defaults.ClientFactory == null)
			{
				throw new InvalidOperationException(ErrorMessages.NoClientFactory);
			}

			Defaults = defaults;
			Request = defaults.ClientFactory.CreateRequest();
		}

		public IApiClientRequest Request { get; set; }

		public RestDefaults Defaults { get; set; }


		/// <summary>
		/// Run the rest request
		/// </summary>
		public RestRequest Response(Action<AssertionBuilder> assert)
		{
			assertions = assert;
			return this;
		}

		public ExecutionResult Execute()
		{
			var clientBuilder = Defaults.ClientFactory;
			if (clientBuilder == null)
			{
				throw new InvalidOperationException(ErrorMessages.NoClientFactory);
			}

			using (var client = clientBuilder.Create())
			{
				var context = new AssertionContext()
				{
					Request = BuildRequest(clientBuilder),
					Client = client,
					ResponseDeserialiser = this.ResponseDeserialiser ?? Defaults.ResponseDeserialiser
				};
				context.Response = client.ExecuteRequest(context.Request);

				return AssertResponse(context, assertions);
			}
		}

		/// <summary>
		/// Execute the assertions and throws an exception if it fails.
		/// </summary>
		public void Assert(Action<AssertionBuilder> assert)
		{
			assertions = assert;
			var results = Execute();
			results.Assert();
		}

		/// <summary>
		/// Execute the assertions and throws an exception if it fails.
		/// </summary>
		public void Assert()
		{
			Assert(assertions);
		}

		public IResponseDeserialiser ResponseDeserialiser { get; set; }


		private ExecutionResult AssertResponse(AssertionContext context, Action<AssertionBuilder> assert)
		{
			var builder = new AssertionBuilder();
			assert(builder);

			var result = new ExecutionResult();
			result.Response = context.Response;

			var assertionResults = new List<AssertionResult>();
			result.Results = assertionResults;

			foreach (var rule in builder.Rules)
			{
				assertionResults.AddRange(rule.Assert(context));
			}

			return result;
		}


		/// <summary>
		/// Creates the request based on the request that we have been manipulating with the extensions
		/// and set the defaults where appropriate.
		/// </summary>
		/// <param name="builder"></param>
		/// <returns></returns>
		private IApiClientRequest BuildRequest(IClientBuilder builder)
		{
			var result = builder.CreateRequest();

			// timeout
			result.TimeoutInSeconds = Request.TimeoutInSeconds;
			if (result.TimeoutInSeconds == 0)
			{
				result.TimeoutInSeconds = Defaults.RequestDefaults.TimeoutInSeconds;
			}

			// headers
			var headers = Request.Headers;
			var defaultHeaders = Defaults.RequestDefaults.Headers;
			if (headers == null)
			{
				headers = defaultHeaders;
			}
			else
			{
				// merging the default headers into the headers to send
				foreach (var k in defaultHeaders.Keys)
				{
					foreach (var v in defaultHeaders[k])
					{
						if (headers.ContainsKey(k))
						{
							if (!headers[k].Contains(v))
							{
								var list = headers[k] as IList<string>;
								if (list == null)
								{
									throw new InvalidOperationException(
										ErrorMessages.InvalidHeaderValueType);
								}
								list.Add(v);
							}
							// If the value already exists then we dont try to add again as it is the default headers

						}
						else
						{
							headers.Add(k, new List<string> { v });
						}
					}
				}
			}
			result.Headers = headers;

			// content
			result.Content = Request.Content;
			if (result.Content == null)
			{
				result.Content = Defaults.RequestDefaults.Content;
			}

			// verb
			result.Verb = Request.Verb;
			if (result.Verb == HttpVerb.Unknown)
			{
				result.Verb = Defaults.RequestDefaults.Verb;
			}


			// uri
			result.Uri = Request.Uri;
			if (result.Uri == null)
			{
				result.Uri = Defaults.RequestDefaults.Uri;
			}

			return result;
		}
	}

	public static class RestRequestSetup
	{
		public static RestRequest UseJsonDeserialiser(this RestRequest request)
		{
			request.ResponseDeserialiser = new JsonResponseDeserialiser();
			return request;
		}

		public static RestRequest WithHeader(this RestRequest request, string key, string value, bool overrideExisting = true)
		{
			var headers = request.Request.Headers;
			if (headers.ContainsKey(key))
			{
				var list = headers[key] as List<string>;
				if (list == null)
				{
					throw new InvalidOperationException(
						ErrorMessages.InvalidHeaderValueType);
				}

				if (overrideExisting && list.Contains(value))
				{
					list.Remove(value);
				}

				list.Add(value);
			}
			else
			{
				var list = new List<string> { value };
				headers.Add(key, list);

			}

			return request;
		}

		public static RestRequest WithJsonBody(this RestRequest request, dynamic obj)
		{
			return WithBody(request, JsonConvert.SerializeObject(obj), "application/json");
		}

		public static RestRequest WithJsonBody(this RestRequest request, string content)
		{
			return WithBody(request, content, "application/json");
		}

		public static RestRequest WithBody(this RestRequest request, string content, string contentTypeHeader = null)
		{
			if (!string.IsNullOrEmpty(contentTypeHeader))
			{
				request.WithHeader("Content-Type", contentTypeHeader);
			}

			request.Request.Content = content;

			return request;
		}


	}
}