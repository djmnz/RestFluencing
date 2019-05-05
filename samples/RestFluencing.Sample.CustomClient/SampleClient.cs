using System;
using System.Net.Http;
using RestFluencing.Assertion;
using RestFluencing.Sample.CustomClient.Client;

namespace RestFluencing.Sample.CustomClient
{
    public class SampleClient
    {
	    public void Test()
	    {
		    var config = 
			    new RestConfiguration()
				    .WithBaseUrl("http://localhost:8080/")
					.UsingMultipartApiClient();

		    config.Post("/")

			    .WithMultipart(r => r.Add(new StringContent("value"), "key"))

			    .Response()
			    .ReturnsStatus(HttpStatusCode.OK);
	    }
    }
}
