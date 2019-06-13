using System;
using System.Net.Http;
using RestFluencing.Assertion;
using RestFluencing.Client;
using RestFluencing.Sample.CustomClient.Client;
using Xunit;

namespace RestFluencing.Sample.CustomClient
{
    public class SampleClient
    {
		[Fact]
	    public void Test()
	    {
			// Steps for creating a new client
			//   1 - Create your client = MultipartContentApiClient
		    //		Optional : override the HttpClientBase
			//   2 - Create your builder = MultipartContentApiClientBuilder
			//   3 - Create your extensions (those are helpers for ease of use - MultiPartContentApiClientBuilderExtensions and MultipartRequestExtensions)
			//   Optional : create your own request type


			var config = 
			    new RestConfiguration()
				    .WithBaseUrl("http://localhost:8080/")
					.UseJsonResponseDeserialiser()
					.UsingMultipartApiClient();

		    config.RequestDefaults.TimeoutInSeconds = 90;

		    config.Post("/")

			    .WithMultipart(r => r.Add(new StringContent("value"), "key"))

			    .Response()
			    .ReturnsStatus(HttpStatusCode.OK);
	    }
    }
}
