using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestFluencing.Client;
using RestFluencing.Helpers;

namespace RestFluencing
{
	/// <summary>
	/// Extensions to help setting up the RestRequest
	/// </summary>
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
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			if (string.IsNullOrEmpty(key))
			{
				throw new ArgumentException(ErrorMessages.HeaderMustHaveKey);
			}

			HeaderHelper.AddHeader(request.Request.Headers, key, value, overrideExisting);

			return request;
		}

		/// <summary>
		/// Serialises the object and sets up the request to send it as a json.
		/// </summary>
		/// <param name="request">Request to add the body content</param>
		/// <param name="obj">Object to be serialised</param>
		/// <param name="settings">Custom settings to use when serialising the object</param>
		/// <returns></returns>
		public static RestRequest WithJsonBody(this RestRequest request, dynamic obj, JsonSerializerSettings settings = null)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}
			settings = settings ?? new JsonSerializerSettings();

			return WithBody(request, JsonConvert.SerializeObject(obj, settings), "application/json");
		}

		/// <summary>
		/// Serialises the object and sets up the request to send it as a json.
		/// </summary>
		/// <param name="request">Request to add the body content</param>
		/// <param name="obj">Object to be serialised</param>
		/// <param name="settings">Custom settings to use when serialising the object</param>
		/// <returns></returns>
		public static RestRequest WithJsonBody<T>(this RestRequest request, T obj, JsonSerializerSettings settings = null)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			settings = settings ?? new JsonSerializerSettings();

			return WithBody(request, JsonConvert.SerializeObject(obj, settings), "application/json");
		}

		/// <summary>
		/// Prepares the request to submit the string content as json
		/// </summary>
		/// <param name="request">Request to be modified</param>
		/// <param name="content">String content. Expected to be JSON as it changes the <code>Content-Type</code> header</param>
		/// <returns></returns>
		public static RestRequest WithJsonBody(this RestRequest request, string content)
		{
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}
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
			if (request == null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			if (!string.IsNullOrEmpty(contentTypeHeader))
			{
				request.WithHeader("Content-Type", contentTypeHeader);
			}

			request.Request.Content = content;

			return request;
		}


	}
}