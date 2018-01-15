using RestFluencing.Assertion;

namespace RestFluencing.Client
{
	public interface IResponseDeserialiser
	{
		object GetResponse(AssertionContext context);

		T GetResponse<T>(AssertionContext context);
	}
}