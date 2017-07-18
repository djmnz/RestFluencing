using Newtonsoft.Json.Linq;
using restfluencing.Assertion;

namespace restfluencing.Client
{
	public interface IResponseDeserialiser
	{
		object GetResponse(AssertionContext context);

		T GetResponse<T>(AssertionContext context);
	}

	public class JsonResponseDeserialiser : IResponseDeserialiser
	{
		private const string BaseResponseKey = "BaseResponse";

		public object GetResponse(AssertionContext context)
		{
			if (context.Properties.ContainsKey(BaseResponseKey))
			{
				return (JToken)context.Properties[BaseResponseKey];
			}
			return JToken.Parse(context.Response.Content);
		}

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