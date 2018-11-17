using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestFluencing.Tests
{
	[TestClass]
	public class BeforeEventsOnRequestTests
	{
		[TestMethod]
		public void WhenThereIsNoEventRegisteredDoesntBreak()
		{
			// Arrange
			var config = RestConfigurationHelper.Default();

			// Act
			config.Get("/null").Response().Assert();
		}

		[TestMethod]
		public void BeforeRequest_WhenAddedOnConfiguration_ShouldCallDelegate()
		{
			// Arrange
			bool callFromConfig = false;
			var config = RestConfigurationHelper.Default();
			config.BeforeRequest(context => { callFromConfig = true; });

			// Act
			config.Get("/null").Response().Assert();

			// Assert
			Assert.IsTrue(callFromConfig);
		}

		[TestMethod]
		public void BeforeRequest_WhenAddedOnRestRequest_ShouldCallDelegate()
		{
			// Arrange
			bool callFromRequest = false;
			var config = RestConfigurationHelper.Default();
			var request = config.Get("/null")
				.BeforeRequest(context => { callFromRequest = true; });

			// Act
			request.Response().Assert();

			// Assert
			Assert.IsTrue(callFromRequest);
		}

		[TestMethod]
		public void BeforeRequest_WhenMultipleDelegates_ShouldCallEachDelegate()
		{
			// Arrange
			bool firstCallFromRequest = false;
			bool secondCallFromRequest = false;
			var config = RestConfigurationHelper.Default();
			var request = config.Get("/null")
				.BeforeRequest(context => { firstCallFromRequest = true; })
				.BeforeRequest(context => { secondCallFromRequest = true; });

			// Act
			request.Response().Assert();

			// Assert
			Assert.IsTrue(firstCallFromRequest);
			Assert.IsTrue(secondCallFromRequest);

		}

		[TestMethod]
		public void BeforeRequest_WhenAddedToConfigThenRequest_ShouldCallEachDelegate()
		{
			// Arrange
			bool firstCallFromConfig = false;
			bool secondCallFromRequest = false;
			var config = RestConfigurationHelper.Default()
				.BeforeRequest(context => { firstCallFromConfig = true; });

			var request = config.Get("/null")
				.BeforeRequest(context => { secondCallFromRequest = true; });

			// Act
			request.Response().Assert();

			// Assert
			Assert.IsTrue(firstCallFromConfig);
			Assert.IsTrue(secondCallFromRequest);

		}

	}
}