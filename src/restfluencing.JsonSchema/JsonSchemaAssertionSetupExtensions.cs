using System;
using Newtonsoft.Json.Schema.Generation;

namespace restfluencing.JsonSchema
{
	public static class JsonSchemaAssertionSetupExtensions
	{
		public static RestResponse HasJsonSchema<T>(this RestResponse builder)
		{
			var schemaRule = new JsonModelSchemaAssertionRule<T>();
			builder.AddRule(schemaRule);
			return builder;
		}
		public static RestResponse HasJsonSchema<T>(this RestResponse builder, JSchemaGenerator generator)
		{
			var schemaRule = new JsonModelSchemaAssertionRule<T>(generator);
			builder.AddRule(schemaRule);
			return builder;
		}
	}
}
