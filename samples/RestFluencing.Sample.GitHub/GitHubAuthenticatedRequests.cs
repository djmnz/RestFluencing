using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Assertion;

namespace RestFluencing.Sample.GitHub
{
	[TestClass]
	public class GitHubAuthenticatedRequests
	{
		private RestConfiguration _configuration;

		[TestInitialize]
		public void SetupCommonConfiguration()
		{
			_configuration = RestConfiguration.JsonDefault();
			_configuration.WithHeader("User-Agent", "RestFluencing Sample");
			_configuration.WithBaseUrl("https://api.github.com/");
			_configuration.WithBasicAuthorization("<username>", "<password>");
		}

		[TestMethod]
		public void GetAuthenticatedValue()
		{
			_configuration.Get("/user/following/defunkt")
				.Response()
				.ReturnsStatus(HttpStatusCode.NotFound)
				.Assert();
		}


	}
}