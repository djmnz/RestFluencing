using System;
using Newtonsoft.Json.Schema.Generation;

namespace RestFluencing.JsonSchema
{
	/// <summary>
	///     Extensions for the RestResponse to allow using the
	/// </summary>
	public static class JsonSchemaAssertionSetupExtensions
	{
		/// <summary>
		///     Verifies whether the response content matches the Schema of the type provided.
		/// </summary>
		/// <typeparam name="T">type to validate against</typeparam>
		/// <param name="response">Response object to add the new rule</param>
		/// <returns></returns>
		public static RestResponse HasJsonSchema<T>(this RestResponse response)
		{
			var schemaRule = new JsonModelSchemaAssertionRule<T>();
			response.AddRule(schemaRule);
			return response;
		}

		/// <summary>
		///     Verifies whether the response content matches the Schema of the type provided.
		/// </summary>
		/// <typeparam name="T">type to validate against</typeparam>
		/// <param name="response">Response object to add the new rule</param>
		/// <param name="generator">Specifies the generator to use</param>
		/// <returns></returns>
		public static RestResponse HasJsonSchema<T>(this RestResponse response, JSchemaGenerator generator)
		{
			var schemaRule = new JsonModelSchemaAssertionRule<T>(generator);
			response.AddRule(schemaRule);
			return response;
		}
	}
}