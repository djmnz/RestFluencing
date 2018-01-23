using Newtonsoft.Json.Linq;
using RestFluencing.Assertion;
using System;

namespace RestFluencing.Client
{
	/// <summary>
	/// Standard Json Deserialiser that stores the value into the context property to the specified type.
	/// </summary>
	public class JsonResponseDeserialiser : IResponseDeserialiser
	{
		private readonly JsonLoadSettings _loadSettings;
		private const string BaseResponseKey = "BaseResponse";
		private static Object _responseLock= new Object();

		/// <summary>
		/// 
		/// </summary>
		public JsonResponseDeserialiser() : this(null)
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="loadSettings"></param>
		public JsonResponseDeserialiser(JsonLoadSettings loadSettings)
		{
			_loadSettings = loadSettings;
		}

		/// <summary>
		/// Get the response as JToken and caches it
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public object GetResponse(AssertionContext context)
		{
			if (context.Properties.ContainsKey(BaseResponseKey))
			{
				return (JToken)context.Properties[BaseResponseKey];
			}

			lock (_responseLock)
			{
				if (context.Properties.ContainsKey(BaseResponseKey))
				{
					return (JToken)context.Properties[BaseResponseKey];
				}

				var responseObject = JToken.Parse(context.Response.Content, _loadSettings);
				context.Properties[BaseResponseKey] = responseObject;
				return responseObject;
			}


		}

		/// <summary>
		/// Get the response as a specified type
		/// </summary>
		/// <typeparam name="T">type to get the response as</typeparam>
		/// <param name="context"></param>
		/// <returns></returns>
		public T GetResponse<T>(AssertionContext context)
		{
			// returns cached value first
			var key = $"TypedResponse.{typeof(T).FullName}";
			if (context.Properties.ContainsKey(key))
			{
				return (T)context.Properties[key];
			}

			// gets the default base response
			var baseResponse = GetResponse(context) as JToken;
			if (baseResponse == null)
			{
				return default(T);
			}

			// converts the base response into the object type
			T result = baseResponse.ToObject<T>();
			context.Properties.Add(key, result);
			return result;
		}

	}
}