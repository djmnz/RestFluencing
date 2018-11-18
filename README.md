# RestFluencing

RestFluencing is a .Net framework for helping writing API Tests in a fluent-style.

It abstracts the creation of network requests and the assertion of its response.

# Current state

Currently used in the Integration Test environment at https://www.starnow.com/ to test their internal Rest APIs.

RestFluencing API is stable and functional, low chance of introducing breaking changes.

Looking for community feedback for closing version the version 0.x as 1.0.

![Build Status](https://ci.appveyor.com/api/projects/status/github/djmnz/restfluencing?branch=master&svg=true)


# Getting started

## Installing

NuGet Package: https://www.nuget.org/packages/RestFluencing/

Installing NuGet Package:

```
Install-Package RestFluencing
```

## First test

``` C#
Rest.GetFromUrl("https://api.github.com/users/defunkt")
	.WithHeader("User-Agent", "RestFluencing Sample")
	.Response()
	.ReturnsStatus(HttpStatusCode.OK)
	.Assert();
```

# Simplifying and Reusing Configuration

RestFluencing is built to help you be more efficient in writing your API tests and you can customise many aspects of the process.

You can create multiple configurations as per scenario and specification and reuse them as necessary.

``` C#
protected RestConfiguration _configuration = null;

[ClassInitialize]
public void SetupCommonConfiguration()
{
	_configuration = RestConfiguration.JsonDefault();
	_configuration.WithHeader("User-Agent", "RestFluencing Sample");
	_configuration.WithBaseUrl("https://api.github.com/");
}
```

The above simplifies the rewriting of the base URL `https://api.github.com/` and adding the `User-Agent` header over again per test.

Then the Getting Started test can become:

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.ReturnsStatus(HttpStatusCode.OK)
	.Assert();
```

## Default Json Configuration

When invoking `RestConfiguration.JsonDefault()` it gives you a configuration instance with the following defaults:

**Headers**: 

`Content-Type: application/json`

`Accept: application/json`


**Deserialiser:** `JsonResponseDeserialiser` (Json.Net)

**ApiClient:** Wrapper for `System.Net.Http.HttpClient`

**Assertion:** `ExceptionAssertion` (can be replaced with `ConsoleAssertion`)


# Asserting Response

`https://api.github.com/users/defunkt`

``` json
{
  "login": "defunkt",
  "id": 2,
  "avatar_url": "https://avatars0.githubusercontent.com/u/2?v=4",
  "gravatar_id": "",
  "url": "https://api.github.com/users/defunkt",
  "html_url": "https://github.com/defunkt",
  "followers_url": "https://api.github.com/users/defunkt/followers",
  "following_url": "https://api.github.com/users/defunkt/following{/other_user}",
  "gists_url": "https://api.github.com/users/defunkt/gists{/gist_id}",
  "starred_url": "https://api.github.com/users/defunkt/starred{/owner}{/repo}",
  "subscriptions_url": "https://api.github.com/users/defunkt/subscriptions",
  "organizations_url": "https://api.github.com/users/defunkt/orgs",
  "repos_url": "https://api.github.com/users/defunkt/repos",
  "events_url": "https://api.github.com/users/defunkt/events{/privacy}",
  "received_events_url": "https://api.github.com/users/defunkt/received_events",
  "type": "User",
  "site_admin": true,
  "name": "Chris Wanstrath",
  "company": "@github ",
  "blog": "http://chriswanstrath.com/",
  "location": "San Francisco",
  "email": null,
  "hireable": true,
  "bio": "🍔 ",
  "public_repos": 107,
  "public_gists": 273,
  "followers": 16673,
  "following": 208,
  "created_at": "2007-10-20T05:24:19Z",
  "updated_at": "2018-01-11T07:04:23Z"
}
```

## Asserting As Dynamic Content

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.ReturnsDynamic(c => c.login == "defunkt", "Login did not match")
	.ReturnsDynamic(c => c.id == 2, "ID did not match")
	.Assert();
```

## Asserting As From Strongly Typed Model

**Model:**
``` C#
public class GitHubUser
{
	public int id { get; set; }
	public string login { get; set; }
}
```

**Test:**

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.Returns<GitHubUser>(c => c.login == "defunkt")
	.Returns<GitHubUser>(c => c.id == 2)
	.Assert();
```

## Asserting Using Custom Function

**Model:**
``` C#
public class GitHubUser
{
	public int id { get; set; }
	public string login { get; set; }
}
```

**Test:**

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.ReturnsModel<GitHubUser>(c => {c.login == "defunkt")
	.ReturnsModel<GitHubUser>(c => c.id == 2)
	.Assert();
```

## Getting the response deserialized object

We are not a Rest client, but an assertion framework. However we understand that in certain circumstances that there may be a need to retrieve the object to be used by the next test.

Below is an example of how you can leverage the custom function assertion to do so.

**Model:**
``` C#
public class GitHubUser
{
	public int id { get; set; }
	public string login { get; set; }
}
```

**Test:**

``` C#

GitHubUserModel user = null;
Rest.GetFromUrl("https://api.github.com/users/defunkt")
	.WithHeader("User-Agent", "RestFluencing Sample")
	.Response()
	.ReturnsModel<GitHubUserModel>(model =>
	{
		user = model;
		return true;
	}, string.Empty)
	.Assert();

Assert.IsNotNull(user);

```

## Intercepting Request before submitted to client

``` C#

// Arrange
bool callFromConfig = false;
var config = RestConfigurationHelper.Default();
config.BeforeRequest(context => { callFromConfig = true; });

// Act
config.Get("/null").Response().Assert();

// Assert
Assert.IsTrue(callFromConfig);

```


## Intercepting Response before it is Asserted

*Note*: If need the deserialize the response to a model, see the example above for `ReturnsModel`.

``` C#
string contentFromConfig = null;
string contentFromRequest = null;
// Arrange
var config = RestConfigurationHelper.Default()
	.AfterRequest(context => { contentFromConfig = context.Response.Content; });

var request = config.Get("/product/apple")
	.AfterRequest(context => { contentFromRequest = context.Response.Content; });

// Act
request.Response().Assert();

// Assert
Assert.IsNotNull(contentFromConfig);
Assert.IsNotNull(contentFromRequest);

Assert.IsTrue(contentFromConfig.Contains("Apple"));
Assert.IsTrue(contentFromRequest.Contains("Apple"));

```

## Asserting Header

**Test:**

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.HasHeader("Content-Type") // this ensures that the header exists
	.HasHeaderValue("Content-Type", "application/json; charset=utf-8") // this asserts that the header exists AND has the value
	.Assert();
```



## Failure Message Example 

**Test**

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.Returns<GitHubUser>(c => c.id == 3)
	.Assert();
```


By default when an assertion rule fails it throws an exception when `Assert()` is called and returns the following message:

```
RestFluencing.AssertionFailedException: Failed with one or more assertions result.

Expression: Response did not return (c.id == 3)


Response: 200 OK
{"login":"defunkt","id":2,"avatar_url":"https://avatars0.githubusercontent.com/u/2?v=4","gravatar_id":"","url":"https://api.github.com/users/defunkt","html_url":"https://github.com/defunkt","followers_url":"https://api.github.com/users/defunkt/followers","following_url":"https://api.github.com/users/defunkt/following{/other_user}","gists_url":"https://api.github.com/users/defunkt/gists{/gist_id}","starred_url":"https://api.github.com/users/defunkt/starred{/owner}{/repo}","subscriptions_url":"https://api.github.com/users/defunkt/subscriptions","organizations_url":"https://api.github.com/users/defunkt/orgs","repos_url":"https://api.github.com/users/defunkt/repos","events_url":"https://api.github.com/users/defunkt/events{/privacy}","received_events_url":"https://api.github.com/users/defunkt/received_events","type":"User","site_admin":true,"name":"Chris Wanstrath","company":"@github ","blog":"http://chriswanstrath.com/","location":"San Francisco","email":null,"hireable":true,"bio":"🍔 ","public_repos":107,"public_gists":273,"followers":16666,"following":208,"created_at":"2007-10-20T05:24:19Z","updated_at":"2018-01-11T07:04:23Z"}

Headers
Content-Length : 1144
Content-Type : application/json; charset=utf-8
Last-Modified : ...
```

## Asserting Model Schema

**This example needs an extra package** `RestFluencing.JsonSchema`

**Test**

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.HasJsonSchema<GitHubUser>()
	.Assert();

```

### Configuring schema validation

The schema validation is based on the JsonSchema by NewtonSoft. You can read its documentation for further validations.

You can pass in your own Schema Validator too:

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.HasJsonSchema<GitHubUser>(new JSchemaGenerator())
	.Assert();
```

And apply any relevant attribute to your model.

# Authenticated Requests

RestFluencing has a few healper methods to assist adding an authorization header.

Note that you will need to implement yourselves how to retrieve the tokens or provide proper values to the header. i.e.  you may want to connect to a database or send an oauth request.

## Bearer

``` C#
request.WithBearerAuthorization("TOKEN");
```

## Basic

``` C#
request.WithBasicAuthorization("USERNAME","PASSWORD");
```

## Custom

``` C#
request.WithAuthorization("AUTHORIZATION HEADER VALUE");
```

## Example

``` C#
_configuration.Get("/user/following/defunkt")
	.WithBasicAuthorization("<username>", "<password>")
	.Response()
	.ReturnsStatus(HttpStatusCode.NotFound)
	.Assert();
```

# Submitting Content Data

## Post/Put Dynamic Models

``` C#
_configuration.Put("/repos/djmnz/RestFluencing/subscription")
	.WithJsonBody(new
	{
		subscribed = true,
		ignored = false
	})
	.Response()
	.ReturnsStatus(HttpStatusCode.OK)
	.ReturnsDynamic(r => r.subscribed == true, "Expected subsribed to be true")
	.Assert();
```


## Post/Put Strongly Typed Models

*Model*

``` C#
public class GitHubSubscriptionModel
{
	public bool subscribed { get; set; }
	public bool ignored { get; set; }
}
```

*Test*

``` C#
_configuration.Put("/repos/djmnz/RestFluencing/subscription")
	.WithJsonBody(new GitHubSubscriptionModel()
	{
		subscribed = true,
		ignored = false
	})
	.Response()
	.ReturnsStatus(HttpStatusCode.OK)
	.Returns<GitHubSubscriptionModel>(r => r.subscribed == true)
	.Assert();
```

