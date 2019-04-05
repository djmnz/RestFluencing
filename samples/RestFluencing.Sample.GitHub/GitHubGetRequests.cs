using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing;
using RestFluencing.Assertion;

namespace RestFluencing.Sample.GitHub
{
	[TestClass]
	public class GitHubGetRequests
	{
		[TestInitialize]
		public void FixTsl()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		}

		[TestMethod]
		public void SimpleGetRequest()
		{
			Rest.GetFromUrl("https://api.github.com/users/defunkt")
				.WithHeader("User-Agent", "RestFluencing Sample")
				.Response()
				.ReturnsStatus(HttpStatusCode.OK)
				.Assert();
		}

		[TestMethod]
		public void SimpleGetRequestNotFound()
		{
			Rest.GetFromUrl("https://api.github.com/XXusers/XXXXXXdefunkt")
				.WithHeader("User-Agent", "RestFluencing Sample")
				.Response()
				.ReturnsStatus(HttpStatusCode.NotFound)
				.Assert();
		}

		[TestMethod]
		public void GetTheModelToPassItOn()
		{
			GitHubUserModel user = null;
			Rest.GetFromUrl("https://api.github.com/users/defunkt")
				.WithHeader("User-Agent", "RestFluencing Sample")
				.Response()
				.ReturnsModel<GitHubUserModel>(model =>
				{
					user = model;
					return true;
				}, string.Empty)
				.Assert();

			Assert.IsNotNull(user);
		}

	}
}
