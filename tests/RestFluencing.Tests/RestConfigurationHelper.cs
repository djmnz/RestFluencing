namespace RestFluencing.Tests
{
	public static class RestConfigurationHelper
	{
		public static RestConfiguration Default()
		{
			// Setup Defaults
			var config = RestConfiguration.JsonDefault();
			config.WithBaseUrl("http://test.starnow.local/");

			// Setup Factory
			var factory = Factories.Default();
			config.ClientFactory = factory;
			return config;
		}
	}
}