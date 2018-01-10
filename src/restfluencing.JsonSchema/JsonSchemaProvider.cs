using System;
using System.Collections.Concurrent;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace restfluencing.JsonSchema
{
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
}