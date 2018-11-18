using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestFluencing.Assertion.Rules;

namespace RestFluencing.Tests
{
	[TestClass]
	public class RestResponseWrapperTest
	{
		private Mock<IRestResponse> _response;

		[TestMethod]
		public void ShouldWrap_AddRule()
		{
			// Arrange
			var wrapper = CreateWrap();

			// Act
			wrapper.AddRule(new BlankResponseAssertionRule("error"));

			// Assert
			_response.Verify(m => m.AddRule(It.IsAny<AssertionRule>()), Times.Once);
			_response.VerifyNoOtherCalls();
		}


		[TestMethod]
		public void ShouldWrap_OnlyOneRuleOf()
		{
			// Arrange
			var wrapper = CreateWrap();

			// Act
			wrapper.OnlyOneRuleOf(new BlankResponseAssertionRule("error"));

			// Assert
			_response.Verify(m => m.OnlyOneRuleOf(It.IsAny<AssertionRule>()), Times.Once);
			_response.VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldWrap_Assert()
		{
			// Arrange
			var wrapper = CreateWrap();

			// Act
			wrapper.Assert();

			// Assert
			_response.Verify(m => m.Assert(), Times.Once);
			_response.VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldWrap_AssertFailure()
		{
			// Arrange
			var wrapper = CreateWrap();

			// Act
			wrapper.AssertFailure();

			// Assert
			_response.Verify(m => m.AssertFailure(), Times.Once);
			_response.VerifyNoOtherCalls();
		}

		[TestMethod]
		public void ShouldWrap_Execute()
		{
			// Arrange
			var wrapper = CreateWrap();
			var expectedResult = new ExecutionResult();
			_response.Setup(m => m.Execute()).Returns(expectedResult);

			// Act
			var result = wrapper.Execute();

			// Assert
			_response.Verify(m => m.Execute(), Times.Once);
			Assert.AreSame(expectedResult, result);
			_response.VerifyNoOtherCalls();
		}

		private RestResponseWrapper CreateWrap()
		{
			_response = new Mock<IRestResponse>();

			return new RestResponseWrapper(_response.Object);

		}
	}
}