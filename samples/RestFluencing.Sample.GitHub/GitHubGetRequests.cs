using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing;
using RestFluencing.Assertion;

namespace RestFluencing.Sample.GitHub
{
	[TestClass]
	public class GitHubGetRequests
	{
		[TestMethod]
		public void SimpleGetRequest()
		{
			Rest.GetFromUrl("https://api.github.com/users/defunkt")
				.WithHeader("User-Agent", "RestFluencing Sample")
				.Response()
				.ReturnsStatus(HttpStatusCode.OK)
				.Assert();
		}

	}
}
