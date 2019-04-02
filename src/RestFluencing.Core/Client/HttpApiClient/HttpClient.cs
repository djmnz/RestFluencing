using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestFluencing.Helpers;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Standard API client that wraps the .Net HttpClient
	/// </summary>
	public class HttpApiClient : IApiClient
	{

		/// <summary>
		/// Executes the request and creates an response object
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		public IApiClientResponse ExecuteRequest(IApiClientRequest request)
		{
			using (HttpClient client = new HttpClient())
			{
				IApiClientResponse result = new ApiClientResponse();

				var httpRequest = new HttpRequestMessage(new HttpMethod(request.Verb.ToString().ToUpper()), request.Uri);
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

				if (request.Content != null)
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

				// Set the timeout just prior to making the request to reduce the risk of unintended overrides (TODO make the HttpClient a singleton)
				client.Timeout = TimeSpan.FromSeconds(request.TimeoutInSeconds);

				using (HttpResponseMessage response = client.SendAsync(httpRequest).GetSyncResult())
				{
					result.Status = (int) response.StatusCode;
					result.StatusCode = (HttpStatusCode) (int) response.StatusCode;
					result.Headers = CreateHeaders(response.Headers);
					result.Content = response.Content.ReadAsStringAsync().GetSyncResult();
					return result;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
		}

		private IDictionary<string, IEnumerable<string>> CreateHeaders(HttpHeaders responseHeaders)
		{
			var result = new Dictionary<string, IEnumerable<string>>();
			
			foreach (var h in responseHeaders)
			{
				result.Add(h.Key, h.Value);
			}

			return result;
		}
	}
}