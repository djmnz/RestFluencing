using System;
using System.Collections.Generic;

namespace RestFluencing.Helpers
{
	internal static class HeaderHelper
	{
		internal static void AddHeader(IDictionary<string, IList<string>> headers, string key, string value, bool overrideExisting)
		{
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

		}

	}
}