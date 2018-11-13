using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing;
using RestFluencing.Assertion;
using RestFluencing.Tests;
using RestFluencing.Tests.Clients;
using RestFluencing.Tests.Models;

namespace RestFluencing.Tests
{
    [TestClass]
    public class ActionRuleTests
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
        public void Action_ShouldBePassedResponse()
        {
            string name = null;

            Rest.Get("/product/apple", _configuration)
                .Response()
                .Action<Product>(p => name = p.Name)
                .Execute()
                .ShouldPass();

            Assert.AreEqual("Apple", name);
        }
    }
}
