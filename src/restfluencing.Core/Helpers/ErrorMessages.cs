using System;

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
		public const string NoConfiguration = "Need to provide a configuration.";
		public const string NoUrl = "Unable to create a request with a null uri";
		public const string NoResponse = "Response is null.";
		public const string NoResult = "Result is null.";
		public const string NoTypeSpecifiedForAssertion = "No Type has been specified for asserting the response.";
		public const string HeaderMustHaveKey = "Header key cannot be empty or null.";

	}
}