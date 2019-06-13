using System.Net.Http;

namespace RestFluencing.Client.MultipartApiClient
{
	/// <summary>
	/// ApiClient Request to use multipart content. This request should be used with the <see cref="MultipartContentApiClient"/>.
	/// </summary>
    public class MultipartFormClientRequest : ApiClientRequest
    {
		/// <summary>
		/// The multipart content of the request.
		/// </summary>
        public MultipartFormDataContent MultipartContent { get; set; } = new MultipartFormDataContent();
    }
}