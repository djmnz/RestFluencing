using Newtonsoft.Json.Linq;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Extensions to help configure the RestConfiguration to use the standard HttpApiClient factory.
	/// </summary>
	public static class HttpApiClientSetupExtensions
	{
		/// <summary>
		/// Uses the standard HttpApiClient wrapper as the web client
		/// </summary>
		/// <param name="config">Configuration to apply to</param>
		/// <returns></returns>
		public static RestConfiguration UsingWebApiClient(this RestConfiguration config)
		{
			config.ClientFactory = new HttpApiClientBuilder();
			return config;
		}

		/// <summary>
		/// Uses the JsonResponseDeserialiser to process the response.
		/// </summary>
		/// <param name="config">Configuration to apply to</param>
		/// <param name="loadSettings">Deserialiser settings to use (null for default)</param>
		/// <returns></returns>
		public static RestConfiguration UseJsonResponseDeserialiser(this RestConfiguration config, JsonLoadSettings loadSettings = null)
		{
			config.ResponseDeserialiser = new JsonResponseDeserialiser(loadSettings);
			return config;
		}
	}
}