using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestFluencing.Tests.Clients;
using RestFluencing.Tests.Models;
using RestFluencing;
using RestFluencing.JsonSchema;

namespace RestFluencing.Tests
{
	[TestClass]
	public class HasJsonSchemaRuleTests
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
		public void SuccessProductModel()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.HasJsonSchema<Product>()
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void SuccessPromoModel()
		{
			Rest.Get("/promo/apple", _configuration)
				.Response()
				.HasJsonSchema<Promo>()
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void SuccessListModel()
		{
			Rest.Get("/product", _configuration)
				.Response()
				.HasJsonSchema<IList<Product>>()
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void SuccessEmptyListModel()
		{
			Rest.Get("/product/empty", _configuration)
				.Response()
				.HasJsonSchema<IList<Product>>()
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void FailOnSingleItemExpectingList()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.HasJsonSchema<IList<Product>>()
				.Execute()
				.ShouldFailForRule<JsonModelSchemaAssertionRule<IList<Product>>>();
		}

		[TestMethod]
		public void FailOnManyItemsButExpectingDifferentModel()
		{
			Rest.Get("/promo", _configuration)
				.Response()
				.HasJsonSchema<IList<Product>>()
				.Execute()
				.ShouldFailForRule<JsonModelSchemaAssertionRule<IList<Product>>>();
		}

		[TestMethod]
		public void FailOnDifferentModelNotArray()
		{
			Rest.Get("/product", _configuration)
				.Response()
				.HasJsonSchema<Product>()
				.Execute()
				.ShouldFailForRule<JsonModelSchemaAssertionRule<Product>>();
		}
	}
}
