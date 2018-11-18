using System;
using RestFluencing.Assertion;
using RestFluencing.Helpers;

namespace RestFluencing
{
	/// <summary>
	/// Extensions for helping configuring the RestConfiguration instance
	/// </summary>
	public static class RestConfigurationSetupExtensions
	{

		/// <summary>
		///		Validates the execution of the rules by throwing an exception.
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static RestConfiguration WithExceptionAssertion(this RestConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}
			config.Assertion = new ExceptionAssertion();
			return config;
		}

		/// <summary>
		///		Validates the execution of the rules by outputting the results in the Console
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public static RestConfiguration WithConsoleAssertion(this RestConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}
			config.Assertion = new ConsoleAssertion();
			return config;
		}

		/// <summary>
		/// Adds a header into the request defaults. 
		/// </summary>
		/// <param name="config">Request to be modified</param>
		/// <param name="headerKey">Header key to add</param>
		/// <param name="headerValue">Value of the key</param>
		/// <param name="overrideExistingValue">If <code>true</code> will dispose of any existing value on the header key</param>
		/// <returns></returns>
		public static RestConfiguration WithHeader(this RestConfiguration config, string headerKey, string headerValue,
			bool overrideExistingValue = true)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config), ErrorMessages.NoConfiguration);
			}

			if (string.IsNullOrEmpty(headerKey))
			{
				throw new ArgumentException(ErrorMessages.HeaderMustHaveKey);
			}

			HeaderHelper.AddHeader(config.RequestDefaults.Headers, headerKey, headerValue, overrideExistingValue);
			return config;
		}

		/// <summary>
		/// Sets up a custom authorisation header. You need to fill the value of the header.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="authorisationHeaderValue">Value to be given as authorisation. This extension does not add any values to the one provided.</param>
		/// <returns></returns>
		public static RestConfiguration WithAuthorization(this RestConfiguration config, string authorisationHeaderValue)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			HeaderHelper.AddHeader(config.RequestDefaults.Headers, DefaultValues.AuthorisationHeaderKey, authorisationHeaderValue, true);
			return config;
		}

		/// <summary>
		/// Sets up a bearer token authorisation header.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="bearerToken">Token to be appended to the header value.</param>
		/// <returns></returns>
		public static RestConfiguration WithBearerAuthorization(this RestConfiguration config, string bearerToken)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			if (bearerToken == null)
			{
				throw new ArgumentNullException(nameof(bearerToken));
			}

			config.WithAuthorization($"Bearer {bearerToken}");

			return config;
		}
		/// <summary>
		/// Sets up a bearer token authorisation header.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="username">Username to generate the basic header authorization</param>
		/// <param name="password">Password to generate the basic header authorization</param>
		/// <returns></returns>
		public static RestConfiguration WithBasicAuthorization(this RestConfiguration config, string username, string password)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			if (username == null)
			{
				throw new ArgumentNullException(nameof(username));
			}


			config.WithAuthorization($"Basic {HeaderHelper.BasicAuthorizationHeaderValue(username, password)}");

			return config;
		}


		/// <summary>
		/// Adds an BeforeRequest Event against this request. Note that you can add more than one event. Events from the Configuration are executed first.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="beforeRequest"></param>
		/// <returns></returns>
		public static RestConfiguration BeforeRequest(this RestConfiguration config, BeforeRequestDelegate beforeRequest)
		{
			config.BeforeRequestEvent += beforeRequest;
			return config;
		}

		/// <summary>
		/// Adds an BeforeRequest Event against this request. Note that you can add more than one event. Events from the configuration are executed first.
		/// </summary>
		/// <param name="config"></param>
		/// <param name="afterRequest"></param>
		/// <returns></returns>
		public static RestConfiguration AfterRequest(this RestConfiguration config, AfterRequestDelegate afterRequest)
		{
			config.AfterRequestEvent += afterRequest;
			return config;
		}


	}
}