using System;
using System.Net.Http;

namespace RestFluencing.Sample.CustomClient.Client
{
	public static class MultipartRequestExtensions
	{
		/// <summary>
		/// Prepares the request body
		/// </summary>
		/// <param name="request">Request to be modified</param>
		/// <param name="content">String content to add to response.</param>
		/// <param name="contentTypeHeader"><code>Content-Type</code> header value</param>
		/// <returns></returns>
		public static RestRequest WithMultipart(this RestRequest request, Action<MultipartFormDataContent> content)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			MultipartFormClientRequest multipart = request.Request as MultipartFormClientRequest;

			if (multipart is null)
			{
				throw new InvalidOperationException("Need to use UsingMultipartApiClient to use multipart requests.");
			}

			request.BeforeRequest(context =>
			{
				MultipartFormClientRequest multipartBeforeRequest = context.Request as MultipartFormClientRequest;

				if (multipartBeforeRequest is null)
				{
					throw new InvalidOperationException(
						"Client factory changed, make sure you are UsingMultipartApiClient.");
				}

				content(multipartBeforeRequest.MultipartContent);
			});

			return request;
		}
	}
}