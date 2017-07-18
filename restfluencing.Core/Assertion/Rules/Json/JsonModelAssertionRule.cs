using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace restfluencing.Assertion.Rules.Json
{
	public class JsonModelAssertionRule : AssertionRule
	{

		private readonly object _instance;


		public JsonModelAssertionRule(object instance) : base("ModelValue")
		{
			_instance = instance;
		}

		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			var responseObj = context.ResponseDeserialiser.GetResponse(context) as JToken;

			return Verify(_instance, responseObj, context, "");
		}

		private IEnumerable<AssertionResult> Verify(object instance,
			JToken responseObj, AssertionContext context, string level)
		{
			if (instance == null)
			{
				if (responseObj != null)
				{
					if (responseObj.Type != JTokenType.Null && responseObj.Type != JTokenType.Undefined)
					{
						yield return new AssertionResult(this, $"Expected null in {level} found {responseObj} of type {responseObj.Type}");
					}
				}

				yield break;
			}
			if (instance.GetType().IsEnumerable())
			{
				if (responseObj.Type != JTokenType.Array)
				{
					yield return new AssertionResult(this, $"Expected an array in {level} found {responseObj} of type {responseObj.Type}");
					yield break;
				}
				var list = instance as IEnumerable;
				int index = 0;
				Func<int, string> indexedName = (i) => $"{level}[{i}]";
				foreach (var instanceValue in list)
				{
					bool found = false;
					foreach (var responseValue in responseObj.Children<JObject>())
					{
						//note : we intentionally don't return the errors found as they are irrelevant
						//       as we are trying to find only the matching object
						if (!Verify(instanceValue, responseValue, context, indexedName(index)).Any())
						{
							found = true;
							break;
						}
					}

					if (!found)
					{
						yield return
							new AssertionResult(this, $"Property {indexedName(index)}: Expected object not found {JObject.FromObject(instanceValue)}");
					}
					index++;
				}
				yield break;
			}

			var meta = MetaProvider.Instance.GetMeta(instance.GetType());

			foreach (var pi in meta.FieldsAndProperties)
			{
				var instVal = meta.GetValue(pi.Name, instance);
				var respProp = responseObj[pi.Name];
				var propName = GetPropName(level, pi);

				if (pi.GetUnderlyingType().IsSimpleType())
				{
					
					var respVal = GetValue(pi.PropertyType, responseObj, pi.Name);
					
					if (instVal != null && !instVal.Equals(respVal))
					{
						yield return new AssertionResult(this, $"Property {propName}: Found value {respProp} expected {instVal}. Found type {respProp?.Type} expected conversion to {pi.PropertyType}");
					}
				}
				else if (pi.GetUnderlyingType().IsEnumerable())
				{
					foreach (var assertionResult in Verify(instVal, respProp, context, propName))
					{
						yield return assertionResult;
					}
				}
				else
				{
					var assertionResults = Verify(instVal, respProp, context,
						propName);
					foreach (var r in assertionResults)
					{
						yield return r;
					}
				}
			}
			//return Enumerable.Empty<AssertionResult>();
		}

		static object GetValue(Type type, JToken obj, string property)
		{
			return type.GetJsonValueMethod().Invoke(obj, new object[] { property });
		}

		private static string GetPropName(string level, PropertyInfo pi)
		{
			return $"{level}{(level != "" ? "." : "")}{pi.Name}";
		}
	}
}