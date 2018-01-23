namespace RestFluencing
{
	/// <summary>
	/// Http verb
	/// </summary>
	public enum HttpVerb
	{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		Unknown = -1,
		Get = 1,
		Post,
		Put,
		Patch,
		Delete,
		Head,
		Options,
		Trace,
		Connect
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}