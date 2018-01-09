using Microsoft.VisualStudio.TestTools.UnitTesting;
using restfluencing.Tests.Clients;
using restfluencing.Tests.Models;
using restfluencing.Assertion;

namespace restfluencing.Tests
{
	[TestClass]
    public class ExpressionAssertionRule
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
	    public void WhenProperty_Equals_ShouldPass()
	    {
		    Rest.Get("/product/1", _configuration)
			    .Response(true)
			    .Returns<Product>(x => x.Name == "Apple")
			    .Execute()
			    .ShouldPass();
	    }

	    [TestMethod]
	    public void WhenDynamicProperty_Equals_ShouldPass()
	    {
		    Rest.Get("/product/1", _configuration)
			    .Response(true)
			    .Returns<dynamic>(x => x.Name == "Apple")
			    .Execute()
			    .ShouldPass();
	    }

	    [TestMethod]
	    public void WhenDynamicProperty_NotEquals_ShouldFail()
	    {
		    Rest.Get("/product/1", _configuration)
			    .Response(true)
			    .Returns<dynamic>(x => x.Name == "NotApple")
			    .Execute()
			    .ShouldFail();
	    }

		[TestMethod]
        public void WhenProperty_NotEquals_ShouldFail()
        {
            Rest.Get("/product/1", _configuration)
                .Response(true)
				.Returns<Product>(x => x.Name == "Fail test")
	            .Execute()
                .ShouldFail();
        }

        [TestMethod]
        public void WhenType_DoesNotMatch_ShouldFail()
        {
            Rest.Get("/product/1", _configuration)
                .Response(true)
	            .Returns<Promo>(x => x.Discount > 0)
	            .Execute()
                .ShouldFail();
        }

    }
}
