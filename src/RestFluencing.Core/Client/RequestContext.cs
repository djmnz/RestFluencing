using System.Collections.Generic;
using RestFluencing.Assertion;

namespace RestFluencing.Client
{
	/// <summary>
	///		Context before a request is submitted to the client
	/// </summary>
	public class RequestContext
	{
		internal RequestContext(AssertionContext context)
		{
			
		}

		/// <summary>
		/// Request used to generate the Response
		/// </summary>
		public IApiClientRequest Request { get; protected set; }

		/// <summary>
		/// Client used to send the Request
		/// </summary>
		public IApiClient Client { get; protected set; }

		/// <summary>
		/// Deserialiser to use for deserialising the Response body
		/// </summary>
		public IResponseDeserialiser ResponseDeserialiser { get; protected set; }

		/// <summary>
		/// Shared properties in the context
		/// </summary>
		public IDictionary<string, object> Properties { get; protected set; }

	}

}