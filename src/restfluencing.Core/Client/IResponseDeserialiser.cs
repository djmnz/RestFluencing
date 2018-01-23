using RestFluencing.Assertion;

namespace RestFluencing.Client
{
	/// <summary>
	/// Interface to help deserialise the response body.
	/// </summary>
	public interface IResponseDeserialiser
	{
		/// <summary>
		/// Deserialise the response body to an object.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		object GetResponse(AssertionContext context);

		/// <summary>
		/// Deserialise the response body to an object.
		/// </summary>
		/// <typeparam name="T">Type to cast to.</typeparam>
		/// <param name="context">Context to use.</param>
		/// <returns></returns>
		T GetResponse<T>(AssertionContext context);
	}
}