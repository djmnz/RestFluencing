using System;

namespace restfluencing
{
	public static class Rest
	{
		public static RestConfiguration Configuration { get; set; } = RestConfiguration.JsonDefault();

		public static RestRequest GetFromUrl(Uri url, RestConfiguration configuration = null)
		{
			return SendToUrl(HttpVerb.Get, url, configuration);
		}

		public static RestRequest PostToUrl(Uri url, RestConfiguration configuration = null)
		{
			return SendToUrl(HttpVerb.Post, url, configuration);
		}

		public static RestRequest SendToUrl(HttpVerb verb, Uri url, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			var request = new RestRequest(configuration)
			{
				Request =
				{
					Uri = url,
					Verb = verb
				}
			};

			return request;
		}

		public static RestRequest GetFromUrl(string url, RestConfiguration configuration = null)
		{
			return GetFromUrl(new Uri(url), configuration);
		}


		public static RestRequest Get(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return GetFromUrl(new Uri(configuration.BaseUrl, relative), configuration);
		}

		public static RestRequest Post(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return PostToUrl(new Uri(configuration.BaseUrl, relative), configuration);
		}


		public static RestRequest Put(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return SendToUrl(HttpVerb.Put, new Uri(configuration.BaseUrl, relative), configuration);
		}

		public static RestRequest Patch(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return SendToUrl(HttpVerb.Patch, new Uri(configuration.BaseUrl, relative), configuration);
		}

		public static RestRequest Delete(string relative, RestConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = Configuration;
			}

			if (configuration.BaseUrl == null)
			{
				throw new ArgumentException("Relative url requests are only available if you set the Default.BaseUrl", "relative");
			}

			return SendToUrl(HttpVerb.Delete, new Uri(configuration.BaseUrl, relative), configuration);
		}

		public static RestConfiguration WithBaseUrl(string url)
		{
			Configuration.WithBaseUrl(url);
			return Configuration;
		}

		public static RestConfiguration WithBaseUrl(Uri url)
		{
			Configuration.WithBaseUrl(url);
			return Configuration;
		}

	}
}