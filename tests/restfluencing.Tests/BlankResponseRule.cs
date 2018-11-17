using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Tests.Clients;
using RestFluencing.Tests.Models;
using RestFluencing;
using RestFluencing.Assertion;
using RestFluencing.Assertion.Rules;
using RestFluencing.JsonSchema;

namespace RestFluencing.Tests
{
	//TODO make the tests only test the new rule and extension
	[TestClass]
	public class BlankResponseRuleTests
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
		public void NotBlankShouldFail()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsEmptyContent()
				.Execute()
				.ShouldFail();
		}

		[TestMethod]
		public void BlankShouldPass()
		{
			Rest.Get("/null", _configuration)
				.Response()
				.ReturnsEmptyContent()
				.Execute()
				.ShouldPass();
		}
	}


	//TODO make the tests only test the new rule and extension
	[TestClass]
	public class NotBlankResponseRuleTests
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
		public void NotBlankShouldPass()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsContent()
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void BlankShouldFail()
		{
			Rest.Get("/null", _configuration)
				.Response()
				.ReturnsContent()
				.Execute()
				.ShouldFail();
		}
	}

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
