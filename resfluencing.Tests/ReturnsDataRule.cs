using Microsoft.VisualStudio.TestTools.UnitTesting;
using resfluencing.Tests.Clients;
using resfluencing.Tests.Models;
using restfluencing;
using restfluencing.Assertion;
using restfluencing.Assertion.Rules.Json;

namespace resfluencing.Tests
{
	[TestClass]
	public class ReturnsDataRule
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
		public void WhenTyped_AndHasAllProperties_ShouldPass()
		{
			var model = new Product()
			{
				Name = "Incomplete model",
				Price = 112.44
			};
			_factory.Responses.Add("/product/3", model);


			Rest.Get("/product/3", _configuration)
				.Response(true)
				.ReturnsData(model)
				.Execute()
				.ShouldPass();
		}
        [TestMethod]
        public void WhenDynamic_AndMatchesProperty_ShouldPass()
        {
            Rest.Get("/product/1", _configuration)
                .Response(true)
                .ReturnsData(new
                {
                    Name = "Apple"
                })
	            .Execute()
                .ShouldPass();
        }
        [TestMethod]
        public void WhenDynamic_AndMatchesProperty_AndMismatchOther_ShouldFail()
        {
            Rest.Get("/product/1", _configuration)
                .Response(true)
                .ReturnsData(new
                {
                    Name = "Apple",
                    Price = 1.241
                })
	            .Execute()
                .ShouldFail();
        }
        [TestMethod]
        public void WhenDynamic_AndPropertyIsArray_AndMatch_ShouldFPass()
        {
            Rest.Get("/product/1", _configuration)
                .Response(true)
                .ReturnsData(new
                {
                    Name = "Apple",
                    Price = 1.241
                })
	            .Execute()
                .ShouldFail();
        }

        [TestMethod]
		public void WhenTyped_AndHasIncompleteData_ShouldFailAsItReturnsAllData()
		{
			Rest.Get("/product/1", _configuration)
				.Response(true)
				.ReturnsData(new Product()
				{
					Name = "Melon"
				})
				.Execute()
				.ShouldFailForRule<JsonModelAssertionRule>();
		}

		[TestMethod]
		public void WhenDynamic_AndDifferentPropertyValue_AndHasIncompleteModel_ShouldFail()
		{
			Rest.Get("/product/1", _configuration)
				.Response(true)
				.ReturnsData(new 
				{
					Name = "Melon"
				})
				.Execute()
				.ShouldFailForRule<JsonModelAssertionRule>();
		}

		[TestMethod]
		public void WhenTyped_AndPropertiesAndModelIsDifferent_ShouldFail()
		{
			Rest.Get("/product/1", _configuration)
				.Response(true)
				.ReturnsData(new Promo()
				{
					Discount = 124
				})
				.Execute()
				.ShouldFailForRule<JsonModelAssertionRule>();
		}
	}
}