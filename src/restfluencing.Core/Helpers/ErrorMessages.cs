namespace RestFluencing.Helpers
{
	internal static class ErrorMessages
	{
		public const string NoClientFactory =
			"The ClientFactory has not been set. Please set the client factory on the Configuration or Request object..";
		public const string NoResponseDeserialiser =
			"The ResponseDeserialiser has not been set. Please set the deserialiser on the Configuration or Request object.";

		public const string InvalidHeaderValueType =
			"The expected type of the Headers values is a List of string. Avoid using Default Headers if you are manipulating the Headers manually.";

		public const string NoAssertion =
			"The Assertion has not been set. Please set the assertion on the Configuration or Request object.";
		public const string BaseUrlIsNotSet =
			"Relative url requests are only available if you set the Configuration.BaseUrl";
	}
}