using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using restfluencing.Assertion;
using restfluencing.Assertion.Rules;

namespace restfluencing.JsonSchema
{
	public class JsonModelSchemaAssertionRule<T> : AssertionRule
	{
		private readonly JSchema schema;
		public JsonModelSchemaAssertionRule() : base("ModelSchema")
		{
			schema = JsonSchemaProvider.Instance.GetSchema<T>();
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			JToken obj = JToken.Parse(context.Response.Content);
			IList<string> messages;
			if (!obj.IsValid(schema, out messages))
			{
				foreach (var m in messages)
				{
					yield return new AssertionResult(this, m);
				}
			}
		}
	}

	internal class JsonSchemaProvider
	{
		private readonly JSchemaGenerator _jSchemaGenerator = new JSchemaGenerator();
		private readonly ConcurrentDictionary<Type, JSchema> _cache = new ConcurrentDictionary<Type, JSchema>();
		private static Object _lock = new Object();


		public static JsonSchemaProvider Instance { get; } = new JsonSchemaProvider();


		public JSchema GetSchema<T>()
		{
			var type = typeof(T);
			JSchema schema;
			if (_cache.TryGetValue(type, out schema))
			{
				return schema;
			}

			lock (_lock)
			{
				if (_cache.TryGetValue(type, out schema))
				{
					return schema;
				}

				schema = _jSchemaGenerator.Generate(type);
				_cache.TryAdd(type, schema);
				return schema;
			}
		}
	}

	public static class JsonSchemaAssertionSetupExtensions
	{

		public static RestResponse HasJsonSchema<T>(this RestResponse builder)
		{
			var schemaRule = new JsonModelSchemaAssertionRule<T>();
			builder.AddRule(schemaRule);
			return builder;
		}
	}
}
