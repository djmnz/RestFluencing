using System;
using RestFluencing.Assertion;
using RestFluencing.Helpers;

namespace RestFluencing
{
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
		/// <param name="request">Request to be modified</param>
		/// <param name="key">Header key to add</param>
		/// <param name="value">Value of the key</param>
		/// <param name="overrideExisting">If <code>true</code> will dispose of any existing value on the header key</param>
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
	}
}