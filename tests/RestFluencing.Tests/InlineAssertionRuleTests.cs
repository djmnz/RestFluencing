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
			_configuration = RestConfigurationHelper.Default();
			_factory = _configuration.ClientFactory as TestApiFactory;
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