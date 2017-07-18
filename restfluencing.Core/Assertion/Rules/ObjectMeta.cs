using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Schema;

namespace restfluencing.Assertion.Rules
{
	internal class ObjectMeta
	{
		private readonly Type _type;
		private readonly IDictionary<string, PropertyInfo> _propCache = new Dictionary<string, PropertyInfo>();
		public IEnumerable<PropertyInfo> FieldsAndProperties { get; }

		public ObjectMeta(Type type)
		{
			_type = type;

			FieldsAndProperties = from pi in _type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
								  where pi.GetIndexParameters().Length == 0
								  select pi;
			foreach (var p in FieldsAndProperties)
			{
				_propCache.Add(p.Name, p);
			}

		}

		public bool TryGetValue(string propertyName, object instance, out object value)
		{
			if (!_propCache.ContainsKey(propertyName))
			{
				value = null;
				return false;
			}
			value = _propCache[propertyName].GetValue(instance, null);
			return true;
		}

		public object GetValue(string propertyName, object instance)
		{
			object result = null;
			if (TryGetValue(propertyName, instance, out result))
			{
				return result;
			}
			return result;
		}
	}


}