using Newtonsoft.Json.Linq;

namespace RestFluencing.Client
{
    public static class JsonResponseExtensions
    {

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