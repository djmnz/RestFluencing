using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using RestFluencing.Client.HttpApiClient;

namespace RestFluencing.Client.MultipartApiClient
{
	/// <summary>
	/// Standard http client that creates a new instance every request.
	/// </summary>
	public class MultipartContentApiClient : HttpApiClientBase
	{
		private readonly HttpClient _reuseClient;
		private HttpClient _disposableClient;

		/// <summary>
		/// Creates the ApiClient with a default reusable http client. Use <code>null</code> for no reuse.
		/// </summary>
		/// <param name="reuseClient"></param>
		public MultipartContentApiClient(HttpClient reuseClient)
		{
			_reuseClient = reuseClient;
		}
		
		/// <inheritdoc />
		protected override void PrepareRequestContent(IApiClientRequest request, HttpRequestMessage httpRequest, string contentType)
		{
			var multiPartContent = request as MultipartFormClientRequest;
			if (multiPartContent is null)
			{
				// default string content behaviour
				base.PrepareRequestContent(request, httpRequest, contentType);
			}
			else 
			{
				// the new multipart content
				httpRequest.Content = multiPartContent.MultipartContent;
			}

		}

		/// <summary>
		/// Creates the client to be used. Override DisposeClient to customise the disposal.
		/// </summary>
		protected override HttpClient CreateClient()
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
		protected override void DisposeClient()
		{
			if (_disposableClient != null)
			{
				_disposableClient.Dispose();
				_disposableClient = null;
			}
		}
	}
}