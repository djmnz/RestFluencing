using RestFluencing.Helpers;
using System;

namespace RestFluencing
{
	/// <summary>
	/// Extensions for the creating a RestRequest from a RestConfiguration
	/// </summary>
	public static class RestConfigurationRequestExtensions
	{
		/// <summary>
		/// Prepare a GET request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		public static RestRequest Get(this RestConfiguration config, string relativeUrl)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (config.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, "relative");
			}

			return Rest.Get(relativeUrl, config);
		}

		/// <summary>
		/// Prepare a POST request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		public static RestRequest Post(this RestConfiguration config, string relativeUrl)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (config.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, nameof(relativeUrl));
			}

			return Rest.Post(relativeUrl, config);
		}

		/// <summary>
		/// Prepare a PATCH request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		public static RestRequest Patch(this RestConfiguration config, string relativeUrl)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (config.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, nameof(relativeUrl));
			}

			return Rest.Patch(relativeUrl, config);
		}

		/// <summary>
		/// Prepare a PUT request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		public static RestRequest Put(this RestConfiguration config, string relativeUrl)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (config.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, nameof(relativeUrl));
			}

			return Rest.Put(relativeUrl, config);
		}

		/// <summary>
		/// Prepare a DELETE request to the URL. You must have set the property <see cref="RestConfiguration.BaseUrl"/> of the configuration
		/// </summary>
		public static RestRequest Delete(this RestConfiguration config, string relativeUrl)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (config.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, nameof(relativeUrl));
			}

			return Rest.Delete(relativeUrl, config);
		}


		/// <summary>
		/// Prepare a request for assertion.
		/// </summary>
		/// <returns></returns>
		public static RestRequest Prepare(this RestConfiguration config, HttpVerb verb, string relativeUrl)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (config.BaseUrl == null)
			{
				throw new ArgumentException(ErrorMessages.BaseUrlIsNotSet, nameof(relativeUrl));
			}

			return Rest.PrepareToUrl(verb, new Uri(config.BaseUrl, relativeUrl), config);
		}

	}
}