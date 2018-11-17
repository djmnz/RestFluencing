using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Assertion;
using RestFluencing.Tests.Clients;
using RestFluencing.Tests.Models;

namespace RestFluencing.Tests
{
	[TestClass]
	public class InlineAssertionRuleTests
	{
		private RestConfiguration _configuration = null;
		private TestApiFactory _factory = null;

		[TestInitialize]
		public void Setup()
		{
			// Setup Defaults
			var restDefaults = RestConfiguration.JsonDefault();
			restDefaults.WithBaseUrl("http://test.starnow.local/");
			_configuration = restDefaults;

			// Setup Factory
			var factory = Factories.Default();
			restDefaults.ClientFactory = factory;
			_factory = factory as TestApiFactory;
		}

		[TestMethod]
		public void WhenBlankShouldFailAssertion()
		{
			var x = "";
			Rest.Get("/product/apple", _configuration)
				.Response()
				.Execute()
				.Assert();
		}

		[TestMethod]
		public void WhenInlineFunctionReturnsFalseShouldFailAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsModel<Product>(product => {
					return
						product.Name == "Hello";
				}, "My custom error message")
				.Execute()
				.ShouldFail();
		}
		[TestMethod]
		public void WhenInlineFunctionReturnsTrueeShouldPassAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsModel<Product>(product => {
					return
						product.Name == "Apple";
				}, "My custom error message")
				.Execute()
				.ShouldPass();
		}


	}
}