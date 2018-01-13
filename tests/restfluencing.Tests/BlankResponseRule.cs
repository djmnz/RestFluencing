using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using restfluencing.Tests.Clients;
using restfluencing.Tests.Models;
using restfluencing;
using restfluencing.Assertion;
using restfluencing.JsonSchema;

namespace restfluencing.Tests
{
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
				.Response(true)
				.IsEmpty()
				.Execute()
				.ShouldFail();
		}

		[TestMethod]
		public void BlankShouldPass()
		{
			Rest.Get("/null", _configuration)
				.Response(true)
				.IsEmpty()
				.Execute()
				.ShouldPass();
		}
	}


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
				.Response(true)
				.IsNotEmpty()
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void BlankShouldFail()
		{
			Rest.Get("/null", _configuration)
				.Response(true)
				.IsNotEmpty()
				.Execute()
				.ShouldFail();
		}
	}

}
