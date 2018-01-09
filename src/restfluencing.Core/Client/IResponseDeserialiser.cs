using restfluencing.Assertion;

namespace restfluencing.Client
{
	public interface IResponseDeserialiser
	{
		object GetResponse(AssertionContext context);

		T GetResponse<T>(AssertionContext context);
	}
}