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

		[TestMethod]
		public void PutAuthenticatedData()
		{
			_configuration.Put("/repos/djmnz/RestFluencing/subscription")
				.WithJsonBody(new
				{
					subscribed = true,
					ignored = false

				})
				.Response()
				.ReturnsStatus(HttpStatusCode.OK)
				.ReturnsDynamic(r => r.subscribed == true, "Expected subsribed to be true")
				.Assert();
		}

		[TestMethod]
		public void PostWithStronglyTypedModel()
		{
			_configuration.Put("/repos/djmnz/RestFluencing/subscription")
				.WithJsonBody(new GitHubSubscriptionModel()
				{
					subscribed = true,
					ignored = false
				})
				.Response()
				.ReturnsStatus(HttpStatusCode.OK)
				.Returns<GitHubSubscriptionModel>(r => r.subscribed == true)
				.Assert();
		}

	}
}