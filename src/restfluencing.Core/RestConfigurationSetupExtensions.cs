using restfluencing.Assertion;

namespace restfluencing
{
	public static class RestConfigurationSetupExtensions
	{

		/// <summary>
		///		Validates the execution of the rules by throwing an exception.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static RestConfiguration WithExceptionAssertion(this RestConfiguration request)
		{
			request.Assertion = new ExceptionAssertion();
			return request;
		}

		/// <summary>
		///		Validates the execution of the rules by outputting the results in the Console
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public static RestConfiguration WithConsoleAssertion(this RestConfiguration request)
		{
			request.Assertion = new ConsoleAssertion();
			return request;
		}

	}
}