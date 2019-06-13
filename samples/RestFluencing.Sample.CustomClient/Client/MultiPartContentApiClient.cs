using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestFluencing.Client;

namespace RestFluencing.Sample.CustomClient.Client
{
    /// <summary>
    /// EXAMPLE
    /// This example uses an old version of the HttpApiClient that was adjusted to produce the proof of concept for
    /// a multipart client.
    /// </summary>
	public class MultipartContentApiClient : IApiClient
    {
        private readonly HttpClient _reuseClient;
        private HttpClient _disposableClient;

        public MultipartContentApiClient(HttpClient reuseClient)
        {
            _reuseClient = reuseClient;
        }

        /// <summary>
        /// Executes the request and creates an response object
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IApiClientResponse ExecuteRequest(IApiClientRequest request)
        {
            IApiClientResponse result = new ApiClientResponse();
            try
            {
                HttpClient client = CreateClient();

                var httpRequest = CreateHttpRequest(request);

                const string contentTypeHeader = "content-type";
                string contentType = null;

                foreach (var h in request.Headers)
                {
                    //because the api keeps overriding the content type we have to find what we defined before
                    IList<string> values;
                    if (h.Key.Equals(contentTypeHeader, StringComparison.InvariantCultureIgnoreCase)
                        && request.Headers.TryGetValue(h.Key, out values))
                    {
                        contentType = values.First();
                    }

                    httpRequest.Headers.TryAddWithoutValidation(h.Key, h.Value);
                }
                
                var multiPartContent = request as MultipartFormClientRequest;
                if (multiPartContent is null) // default string content behaviour
                {
                    if (contentType != null)
                    {
                        httpRequest.Content = new StringContent(request.Content, Encoding.UTF8, contentType);
                    }
                    else
                    {
                        httpRequest.Content = new StringContent(request.Content);
                    }
                }
                else // the new multipart content
                {
                    httpRequest.Content = multiPartContent.MultipartContent;
                }

                // Set the timeout just prior to making the request to reduce the risk of unintended overrides
                client.Timeout = TimeSpan.FromSeconds(request.TimeoutInSeconds);

                using (HttpResponseMessage response = client.SendAsync(httpRequest).GetAwaiter().GetResult())
                {
                    result.Status = (int)response.StatusCode;
                    result.StatusCode = (HttpStatusCode)(int)response.StatusCode;
                    result.Headers = CreateHeaders(response.Headers, response.Content.Headers);
                    result.Content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    return result;
                }

            }
            finally
            {
                DisposeClient();
            }


        }

        /// <summary>
        /// Creates the HttpRequestMessage from the IApiClientRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual HttpRequestMessage CreateHttpRequest(IApiClientRequest request)
        {
            return new HttpRequestMessage(new HttpMethod(request.Verb.ToString().ToUpper()), request.Uri);
        }

        /// <summary>
        /// Creates the client to be used. Override DisposeClient to customise the disposal.
        /// </summary>
        protected HttpClient CreateClient()
        {
            if (_reuseClient == null)
            {
                _disposableClient = new HttpClient();
                return _disposableClient;
            }

            return _reuseClient;
        }

        /// <summary>
        /// Disposes the client.
        /// </summary>
        protected void DisposeClient()
        {
            if (_disposableClient != null)
            {
                _disposableClient.Dispose();
                _disposableClient = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }


        private IDictionary<string, IEnumerable<string>> CreateHeaders(
            HttpResponseHeaders responseHeaders,
            HttpHeaders contentHeaders)
        {
            var result = new Dictionary<string, IEnumerable<string>>();

            foreach (var h in responseHeaders)
            {
                result.Add(h.Key, h.Value);
            }

            foreach (var h in contentHeaders)
            {
                result.Add(h.Key, h.Value);
            }

            return result;
        }
    }
}