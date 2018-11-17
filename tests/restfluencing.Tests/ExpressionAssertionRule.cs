using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Tests.Clients;
using RestFluencing.Tests.Models;
using RestFluencing.Assertion;

namespace RestFluencing.Tests
{
	[TestClass]
    public class ExpressionAssertionRuleTests
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
	    public void WhenProperty_Equals_ShouldPass()
	    {
		    Rest.Get("/product/apple", _configuration)
			    .Response()
			    .Returns<Product>(x => x.Name == "Apple")
			    .Execute()
			    .ShouldPass();
	    }

	    [TestMethod]
	    public void WhenDynamicProperty_Equals_ShouldPass()
	    {
		    Rest.Get("/product/apple", _configuration)
			    .Response()
			    .ReturnsDynamic(x => x.Name == "Apple", "Name does not match")
			    .Execute()
			    .ShouldPass();
	    }

	    [TestMethod]
	    public void WhenDynamicProperty_NotEquals_ShouldFail()
	    {
		    Rest.Get("/product/apple", _configuration)
			    .Response()
			    .ReturnsDynamic(x => x.Name == "NotApple", "Name does not match")
			    .Execute()
			    .ShouldFail();
	    }

		[TestMethod]
        public void WhenProperty_NotEquals_ShouldFail()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
				.Returns<Product>(x => x.Name == "Fail test")
	            .Execute()
                .ShouldFail();
        }

        [TestMethod]
        public void WhenType_DoesNotMatch_ShouldFail()
        {
            Rest.Get("/product/apple", _configuration)
                .Response()
	            .Returns<Promo>(x => x.Discount > 0)
	            .Execute()
                .ShouldFail();
        }


    }
}
