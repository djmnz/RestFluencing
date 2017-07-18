using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using resfluencing.Tests.Clients;
using resfluencing.Tests.Models;
using restfluencing;
using restfluencing.Assertion;
using restfluencing.Assertion.Rules.Json;

namespace resfluencing.Tests
{
	[TestClass]
	public class HasJsonSchemaRule
	{
		private RestDefaults _default = null;
		private TestApiFactory _factory = null;


		[TestInitialize]
		public void Setup()
		{
			// Setup Defaults
			var restDefaults = RestDefaults.JsonDefault();
			restDefaults.WithBaseUrl("http://test.starnow.local/");
			_default = restDefaults;

			// Setup Factory
			var factory = Factories.Default();
			restDefaults.ClientFactory = factory;
			_factory = factory as TestApiFactory;
		}

		[TestMethod]
		public void SuccessProductModel()
		{
			Rest.Get("/product/1", _default)
				.Response(a => a
					.HasJsonSchema<Product>()
				)
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void SuccessPromoModel()
		{
			Rest.Get("/promo/1", _default)
				.Response(a => a
					.HasJsonSchema<Promo>()
				)
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void SuccessListModel()
		{
			Rest.Get("/product", _default)
				.Response(a => a
					.HasJsonSchema<IList<Product>>()
				)
				.Execute()
				.ShouldPass();
		}

		[TestMethod]
		public void SuccessEmptyListModel()
		{
			Rest.Get("/product/empty", _default)
				.Response(a => a
					.HasJsonSchema<IList<Product>>()
				)
				.Execute()
				.ShouldPass();
		}

	    [TestMethod]
	    public void FailOnSingleItemExpectingList()
	    {
	        Rest.Get("/product/1", _default)
	            .Response(a => a
	                .HasJsonSchema<IList<Product>>()
	            )
		        .Execute()
	            .ShouldFailForRule<JsonModelSchemaAssertionRule<IList<Product>>>();
	    }

	    [TestMethod]
	    public void FailOnManyItemsButExpectingDifferentModel()
	    {
	        Rest.Get("/promo", _default)
	            .Response(a => a
	                .HasJsonSchema<IList<Product>>()
	            )
		        .Execute()
	            .ShouldFailForRule<JsonModelSchemaAssertionRule<IList<Product>>>();
	    }

        [TestMethod]
        public void FailOnDifferentModelNotArray()
        {
            Rest.Get("/product", _default)
                .Response(a => a
                    .HasJsonSchema<Product>()
                )
	            .Execute()
                .ShouldFailForRule<JsonModelSchemaAssertionRule<Product>>();
        }
    }
}
