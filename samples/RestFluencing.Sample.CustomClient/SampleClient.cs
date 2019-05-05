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
