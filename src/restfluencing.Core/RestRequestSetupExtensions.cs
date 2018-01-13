using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using restfluencing.Client;
using restfluencing.Helpers;

namespace restfluencing
{
	public static class RestRequestSetupExtensions
	{
		/// <summary>
		/// Adds a header into the request. 
		/// </summary>
		/// <param name="request">Request to be modified</param>
		/// <param name="key">Header key to add</param>
		/// <param name="value">Value of the key</param>
		/// <param name="overrideExisting">If <code>true</code> will dispose of any existing value on the header key</param>
		/// <returns></returns>
		public static RestRequest WithHeader(this RestRequest request, string key, string value, bool overrideExisting = true)
		{
			var headers = request.Request.Headers;
			if (headers.ContainsKey(key))
			{
				var list = headers[key] as List<string>;
				if (list == null)
				{
					throw new InvalidOperationException(
						ErrorMessages.InvalidHeaderValueType);
				}

				if (overrideExisting && list.Contains(value))
				{
					list.Remove(value);
				}

				list.Add(value);
			}
			else
			{
				var list = new List<string> { value };
				headers.Add(key, list);

			}

			return request;
		}

		/// <summary>
		/// Serialises the object and sets up the request to send it as a json.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static RestRequest WithJsonBody(this RestRequest request, dynamic obj)
		{
			return WithBody(request, JsonConvert.SerializeObject(obj), "application/json");
		}

		/// <summary>
		/// Prepares the request to submit the string content as json
		/// </summary>
		/// <param name="request">Request to be modified</param>
		/// <param name="content">String content. Expected to be JSON</param>
		/// <returns></returns>
		public static RestRequest WithJsonBody(this RestRequest request, string content)
		{
			return WithBody(request, content, "application/json");
		}

		/// <summary>
		/// Prepares the request body
		/// </summary>
		/// <param name="request">Request to be modified</param>
		/// <param name="content">String content to add to response.</param>
		/// <param name="contentTypeHeader"><code>Content-Type</code> header value</param>
		/// <returns></returns>
		public static RestRequest WithBody(this RestRequest request, string content, string contentTypeHeader = null)
		{
			if (!string.IsNullOrEmpty(contentTypeHeader))
			{
				request.WithHeader("Content-Type", contentTypeHeader);
			}

			request.Request.Content = content;

			return request;
		}


	}
}