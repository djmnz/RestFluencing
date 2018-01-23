namespace RestFluencing.Assertion
{
	/// <summary>
	/// Interface to process the results of the assertion.
	/// </summary>
	public interface IAssertion
	{
		/// <summary>
		/// Assert method to assert the result
		/// </summary>
		/// <param name="result">result to be asserted</param>
		void Assert(ExecutionResult result);
	}
}