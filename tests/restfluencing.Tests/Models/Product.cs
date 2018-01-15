using System;
using System.Collections.Generic;

namespace RestFluencing.Tests.Models
{
	public class Product
	{
		public string Name { get; set; }
		public DateTime ExpiryDateTime { get; set; }

		public double Price { get; set; }

		public IList<string> Sizes { get; set; }

	}
}