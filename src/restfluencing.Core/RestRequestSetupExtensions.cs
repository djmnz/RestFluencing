using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using restfluencing.Client;
using restfluencing.Helpers;

namespace restfluencing
{
	public static class RestRequestSetupExtensions
	{
		public static RestRequest UseJsonDeserialiser(this RestRequest request)
		{
			request.ResponseDeserialiser = new JsonResponseDeserialiser();
			return request;
		}

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

		public static RestRequest WithJsonBody(this RestRequest request, dynamic obj)
		{
			return WithBody(request, JsonConvert.SerializeObject(obj), "application/json");
		}

		public static RestRequest WithJsonBody(this RestRequest request, string content)
		{
			return WithBody(request, content, "application/json");
		}

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