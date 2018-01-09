using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace restfluencing.Assertion.Rules
{
	internal static class TypeExtensions
	{
		private static readonly MethodInfo _method = typeof(JToken).GetMethod("Value");
		private static readonly Dictionary<Type, MethodInfo> _genericMethodCache = new Dictionary<Type, MethodInfo>();
		private static readonly Dictionary<Type, bool> _isEnumerableCache = new Dictionary<Type, bool>();


		/// <summary>
		/// Determine whether a type is simple (String, Decimal, DateTime, etc) 
		/// or complex (i.e. custom class with public properties and methods).
		/// </summary>
		/// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
		public static bool IsSimpleType(
			this Type type)
		{
			return
				type.IsValueType ||
				type.IsPrimitive ||
				new[]
				{
					typeof(String),
					typeof(Decimal),
					typeof(DateTime),
					typeof(DateTimeOffset),
					typeof(TimeSpan),
					typeof(Guid)
				}.Contains(type) ||
				(Convert.GetTypeCode(type) != TypeCode.Object);
		}

		public static Type GetUnderlyingType(this MemberInfo member)
		{
			switch (member.MemberType)
			{
				case MemberTypes.Event:
					return ((EventInfo)member).EventHandlerType;
				case MemberTypes.Field:
					return ((FieldInfo)member).FieldType;
				case MemberTypes.Method:
					return ((MethodInfo)member).ReturnType;
				case MemberTypes.Property:
					return ((PropertyInfo)member).PropertyType;
				default:
					throw new ArgumentException
					(
						"Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
					);
			}
		}


		public static MethodInfo GetJsonValueMethod(this Type type)
		{
			if (_genericMethodCache.ContainsKey(type))
			{
				return _genericMethodCache[type];
			}

			var makeGenericMethod = _method.MakeGenericMethod(type);
			_genericMethodCache.Add(type, makeGenericMethod);
			return makeGenericMethod;

		}



		public static bool IsEnumerable(this Type type)
		{
			if (_isEnumerableCache.ContainsKey(type))
			{
				return _isEnumerableCache[type];
			}
			bool result = type.IsArray;
			if (!result)
			{
				result = type.GetInterfaces().Any(
					ti => ti == typeof(IEnumerable));
			}
			_isEnumerableCache.Add(type, result);
			return result;
		}
	}
}