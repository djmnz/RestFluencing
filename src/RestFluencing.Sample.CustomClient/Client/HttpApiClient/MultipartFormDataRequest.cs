using System.Net.Http;

namespace RestFluencing.Client.HttpApiClient
{
    public class MultipartFormDataRequest : ApiClientRequest
    {
        public MultipartFormDataContent MultipartContent { get; set; }
    }
}