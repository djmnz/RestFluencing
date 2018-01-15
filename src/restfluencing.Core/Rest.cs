using RestFluencing.Helpers;
using System;

namespace RestFluencing
{
	/// <summary>
	/// Rest framework entry class.
	/// Use the Configuration property to apply the default configuration.
	/// </summary>
	public static class Rest
	{
		/// <summary>
		/// Default configuration for when none is defined in a Rest request.
		/// </summary>
		public static RestConfiguration Configuration { get; set; } = RestConfiguration.JsonDefault();

		/// <summary>
		/// Prepare a GET request to the URL
		/// </summary>
		/// <param name="url">URL to send the request to</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		/// <returns></returns>
		public static RestRequest GetFromUrl(Uri url, RestConfiguration configuration = null)
		{
			return SendToUrl(HttpVerb.Get, url, configuration);
		}

		/// <summary>
		/// Prepare a POST request to the URL
		/// </summary>
		/// <param name="url">URL to send the request to</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		/// <returns></returns>
		public static RestRequest PostToUrl(Uri url, RestConfiguration configuration = null)
		{
			return SendToUrl(HttpVerb.Post, url, configuration);
		}

		/// <summary>
		/// Prepare a specific request
		/// </summary>
		/// <param name="verb">Verb to use</param>
		/// <param name="url">URL to send the request to</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest SendToUrl(HttpVerb verb, Uri url, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			var request = new RestRequest(configuration)
			{
				Request =
				{
					Uri = url,
					Verb = verb
				}
			};

			return request;
		}

		/// <summary>
		/// Prepare a GET request to the URL
		/// </summary>
		/// <param name="url">URL to send the request to</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest GetFromUrl(string url, RestConfiguration configuration = null)
		{
			return GetFromUrl(new Uri(url), configuration);
		}


		/// <summary>
		/// Prepare a GET request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		/// <param name="relative">Relative URL to append into the <see cref="RestConfiguration.BaseUrl"/> of the configuration</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest Get(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, "relative");
			}

			return GetFromUrl(new Uri(configuration.BaseUrl, relative), configuration);
		}

		/// <summary>
		/// Prepare a POST request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		/// <param name="relative">Relative URL to append into the <see cref="RestConfiguration.BaseUrl"/> of the configuration</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest Post(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, "relative");
			}

			return PostToUrl(new Uri(configuration.BaseUrl, relative), configuration);
		}


		/// <summary>
		/// Prepare a PUT request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		/// <param name="relative">Relative URL to append into the <see cref="RestConfiguration.BaseUrl"/> of the configuration</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest Put(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, "relative");
			}

			return SendToUrl(HttpVerb.Put, new Uri(configuration.BaseUrl, relative), configuration);
		}

		/// <summary>
		/// Prepare a PATCH request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		/// <param name="relative">Relative URL to append into the <see cref="RestConfiguration.BaseUrl"/> of the configuration</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest Patch(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, "relative");
			}

			return SendToUrl(HttpVerb.Patch, new Uri(configuration.BaseUrl, relative), configuration);
		}

		/// <summary>
		/// Prepare a DELETE request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		/// <param name="relative">Relative URL to append into the <see cref="RestConfiguration.BaseUrl"/> of the configuration</param>
		/// <param name="configuration">Configuration to apply to the request - null to use <see cref="Configuration"/></param>
		public static RestRequest Delete(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, "relative");
			}

			return SendToUrl(HttpVerb.Delete, new Uri(configuration.BaseUrl, relative), configuration);
		}

	}
}