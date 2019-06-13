using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestFluencing.Helpers;

namespace RestFluencing.Client.HttpApiClient
{
	/// <summary>
	/// Base client class to help extending the behaviour of the client that RestFluencing is to use.
	/// </summary>
	public abstract class HttpApiClientBase : IApiClient
	{
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

				PrepareRequestHeaders(request, httpRequest);

				var contentType = GetContentTypeHeaderValue(request);

				PrepareRequestContent(request, httpRequest, contentType);

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
		/// Prepares the request content. By default, creates a string content with the provided contentType.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="httpRequest"></param>
		/// <param name="contentType"></param>
		protected virtual void PrepareRequestContent(IApiClientRequest request, HttpRequestMessage httpRequest,
			string contentType)
		{
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
		}

		/// <summary>
		/// Prepares the header requests, copying from the RestFluencing Request to the HttpRequestMessage.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="httpRequest"></param>
		protected virtual void PrepareRequestHeaders(IApiClientRequest request, HttpRequestMessage httpRequest)
		{
			foreach (var h in request.Headers)
			{
				httpRequest.Headers.TryAddWithoutValidation(h.Key, h.Value);
			}
		}

		/// <summary>
		/// Retrieves the Content Type header defined in the RestFluencing Request.
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		protected virtual string GetContentTypeHeaderValue(IApiClientRequest request)
		{
			//because the api keeps overriding the content type we have to find what we defined before
			const string contentTypeHeader = "content-type";
			string contentType = null;

			foreach (var h in request.Headers)
			{
				IList<string> values;
				if (h.Key.Equals(contentTypeHeader, StringComparison.InvariantCultureIgnoreCase)
					&& request.Headers.TryGetValue(h.Key, out values))
				{
					contentType = values.First();
				}
			}

			return contentType;

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
		protected abstract HttpClient CreateClient();

		/// <summary>
		/// Disposes the client.
		/// </summary>
		protected abstract void DisposeClient();
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