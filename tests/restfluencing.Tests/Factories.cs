using System;
using System.Collections.Generic;
using RestFluencing.Tests.Clients;
using RestFluencing.Tests.Models;
using RestFluencing.Client;

namespace RestFluencing.Tests
{
	public static class Factories
	{
		public static IClientBuilder Default()
		{
			var factory = new TestApiFactory();


			// Products
			var appleProduct = new Product()
			{
				Name = "Apple",
				ExpiryDateTime = new DateTime(2017, 1, 23, 10, 23, 55, DateTimeKind.Utc),
				Price = 1.24,
				Sizes = new List<string>() { "Small", "Large" }
			};


			var melonProduct = new Product()
			{
				Name = "Melon",
				ExpiryDateTime = new DateTime(2017, 1, 23, 10, 23, 55, DateTimeKind.Utc),
				Price = 10,
				Sizes = new List<string>() { "Large" }
			};

		
			

            // Promos

		    var melonPromo = new Promo()
		    {
		        Product = melonProduct,
		        Discount = 33.45
		    };

		    var applePromo = new Promo()
		    {
		        Product = appleProduct,
		        Discount = 11
		    };

            factory.Responses.Add("/promo/melon", melonPromo);

		    factory.Responses.Add("/promo/apple", applePromo);

            factory.Responses.Add("/promo", new List<Promo>()
            {
                applePromo, melonPromo
            });

            factory.Responses.Add("/product/apple", appleProduct);

			factory.Responses.Add("/product", new List<Product>
			{
				melonProduct,
				appleProduct
			});

			factory.Responses.Add("/product/empty", new List<Product>
			{
			});


			factory.Responses.Add("/null", null);

			return factory;
		}
	}
}