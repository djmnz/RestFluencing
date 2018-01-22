using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Assertion;
using RestFluencing.JsonSchema;

namespace RestFluencing.Sample.GitHub
{
	[TestClass]
	public class GitHubCommonConfiguration
	{
		protected RestConfiguration _configuration = null;

		[TestInitialize]
		public void SetupCommonConfiguration()
		{
			_configuration = RestConfiguration.JsonDefault();
			_configuration.WithHeader("User-Agent", "RestFluencing Sample");
			_configuration.WithBaseUrl("https://api.github.com/");
		}

		[TestMethod]
		public void AssertDynamicResponse()
		{
			_configuration.Get("/users/defunkt")
				.Response()
				.ReturnsDynamic(c => c.login == "defunkt", "Login did not match")
				.ReturnsDynamic(c => c.id == 2, "ID did not match")
				.Assert();
		}

		[TestMethod]
		public void AssertingModelDataResponse()
		{
			_configuration.Get("/users/defunkt")
				.Response()
				.Returns<GitHubUser>(c => c.login == "defunkt")
				.Returns<GitHubUser>(c => c.id == 2)
				.Assert();
		}

		[TestMethod]
		public void AssertHeadersFromResponse()
		{
			_configuration.Get("/users/defunkt")
				.Response()
				.HasHeader("Content-Type") // alternatively you can look for a value:
				.HasHeaderValue("Content-Type", "application/json; charset=utf-8")
				.Assert();
		}

		[TestMethod]
		public void AssertModelSchema()
		{
			_configuration.Get("/users/defunkt")
				.Response()
				.HasJsonSchema<GitHubUser>()
				.Assert();
		}
	}

	public class GitHubUser
	{
		public int id { get; set; }
		public string login { get; set; }
	}

}