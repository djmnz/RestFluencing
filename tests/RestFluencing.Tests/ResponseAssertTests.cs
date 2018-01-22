using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestFluencing.Assertion;
using RestFluencing.Tests.Clients;

namespace RestFluencing.Tests
{
	// TODO create mocks for success and failure rules so we dont test the individual rules here too
	[TestClass]
	public class ResponseAssertTests
	{
		private RestConfiguration _configuration = null;
		private TestApiFactory _factory = null;
		private Mock<IAssertion> _assertion = null;


		[TestInitialize]
		public void Setup()
		{
			// Setup Defaults
			var restDefaults = RestConfiguration.JsonDefault();
			restDefaults.WithBaseUrl("http://test.starnow.local/");
			_configuration = restDefaults;
			_assertion = new Mock<IAssertion>();
			_configuration.Assertion = _assertion.Object;

			// Setup Factory
			var factory = Factories.Default();
			restDefaults.ClientFactory = factory;
			_factory = factory as TestApiFactory;

		}


		[TestMethod]
		public void WhenAssert_AndNoFailures_ThenShouldNotCallTheAssertion()
		{

			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsStatus(HttpStatusCode.OK)
				.Assert();

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Never);
		}

		[TestMethod]
		public void WhenAssert_AndFailures_ThenShouldCallTheAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsStatus(HttpStatusCode.Accepted)
				.Assert();

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Once);
		}

		[TestMethod]
		public void WhenAssertFailure_AndNoFailures_ThenShouldCallTheAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsStatus(HttpStatusCode.OK)
				.AssertFailure();

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Once);
		}

		[TestMethod]
		public void WhenAssertFailure_AndFailures_ThenShouldNotCallTheAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response()
				.ReturnsStatus(HttpStatusCode.Accepted)
				.AssertFailure();

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Never);
		}


		[TestMethod]
		public void WhenAutoAssert_AndFailures_ThenShouldCallTheAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response(true)
				.ReturnsStatus(HttpStatusCode.Accepted);

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Once);
		}


		[TestMethod]
		public void WhenAutoAssert_AndNoFailures_ThenShouldNotCallTheAssertion()
		{
			Rest.Get("/product/apple", _configuration)
				.Response(true)
				.ReturnsStatus(HttpStatusCode.OK);

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Never);
		}

		[TestMethod]
		public void WhenAutoAssert_ThenAssert_AndFailures_ThenShouldCallTheAssertionTwice()
		{
			//note : should call twice because it is an auto assert and also a manual assert
			Rest.Get("/product/apple", _configuration)
				.Response(true)
				.ReturnsStatus(HttpStatusCode.Accepted) // call 1
				.Assert(); // call 2

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Exactly(2));
		}

		[TestMethod]
		public void WhenAutoAssert_AndMultipleFailures_ThenShouldCallTheAssertionThree()
		{
			//note : should call twice because it is an auto assert and also a manual assert
			Rest.Get("/product/apple", _configuration)
				.Response(true)
				.ReturnsStatus(HttpStatusCode.Accepted) // call 1
				.ReturnsDynamic(x => x.a == "a", "Not a") // call 2
				.Assert(); // call 3

			_assertion.Verify(m => m.Assert(It.IsAny<ExecutionResult>()), Times.Exactly(3));
		}

	}
}