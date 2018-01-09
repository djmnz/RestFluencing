using restfluencing.Assertion;

namespace restfluencing
{
	public static class RestConfigurationSetupExtensions
	{

		public static RestConfiguration WithExceptionAssertion(this RestConfiguration request)
		{
			request.Assertion = new ExceptionAssertion();
			return request;
		}

		public static RestConfiguration WithConsoleAssertion(this RestConfiguration request)
		{
			request.Assertion = new ConsoleAssertion();
			return request;
		}

		public static RestConfiguration WithJsonDeserialiser(this RestConfiguration request)
		{
			request.Assertion = new ExceptionAssertion();
			return request;
		}

	}
}