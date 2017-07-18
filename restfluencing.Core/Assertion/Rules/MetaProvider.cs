using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace restfluencing.Assertion.Rules
{
	internal class MetaProvider
	{
		private readonly ConcurrentDictionary<Type, ObjectMeta> _cache = new ConcurrentDictionary<Type, ObjectMeta>();
		private static readonly Object _lock = new Object();


		public static MetaProvider Instance { get; } = new MetaProvider();


		public ObjectMeta GetMeta(Type type)
		{
			ObjectMeta meta;
			if (_cache.TryGetValue(type, out meta))
			{
				return meta;
			}

			lock (_lock)
			{
				if (_cache.TryGetValue(type, out meta))
				{
					return meta;
				}

				meta = new ObjectMeta(type);
				_cache.TryAdd(type, meta);
				return meta;
			}
		}




	}
}