using System;
using System.Collections.Generic;
using resfluencing.Tests.Clients;
using resfluencing.Tests.Models;
using restfluencing.Client;

namespace resfluencing.Tests
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


			var melonproduct = new Product()
			{
				Name = "Melon",
				ExpiryDateTime = new DateTime(2017, 1, 23, 10, 23, 55, DateTimeKind.Utc),
				Price = 10,
				Sizes = new List<string>() { "Large" }
			};

            // Promos

		    var melonPromo = new Promo()
		    {
		        Product = melonproduct,
		        Discount = 33.45
		    };

		    var applePromo = new Promo()
		    {
		        Product = melonproduct,
		        Discount = 33.45
		    };

            factory.Responses.Add("/promo/1", melonPromo);

		    factory.Responses.Add("/promo/2", applePromo);

            factory.Responses.Add("/promo", new List<Promo>()
            {
                applePromo, melonPromo
            });

            factory.Responses.Add("/product/1", appleProduct);

			factory.Responses.Add("/product", new List<Product>
			{
				melonproduct,
				appleProduct
			});

			factory.Responses.Add("/product/empty", new List<Product>
			{
			});

			return factory;
		}
	}
}