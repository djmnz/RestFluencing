using System.Net.Http;
using RestFluencing.Client;

namespace RestFluencing.Sample.CustomClient.Client
{
    public class MultipartFormClientRequest : ApiClientRequest
    {
        public MultipartFormDataContent MultipartContent { get; set; }
    }
}