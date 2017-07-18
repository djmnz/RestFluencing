namespace restfluencing.Helpers
{
	internal static class ErrorMessages
	{
		public const string NoClientFactory =
			"The ClientFactory has not been set. Please set the client factory that you want to use against the request or the defaults.";

		public const string InvalidHeaderValueType =
			"The expected type of the Headers values is a List of string. Avoid using Default Headers if you are manipulating the Headers manually.";
	}

	internal static class DefaultValues
	{
		public static readonly int TimeOutInSeconds = 30;
	}
}