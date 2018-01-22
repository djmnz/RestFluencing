# RestFluencing
# Under Construction

RestFluencing is a .Net framework for helping writing API Tests in a fluent-style.

It abstracts the creation of network requests and the assertion of its response.

# Getting started

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

*Headers*: 
`Content-Type: application/json`
`Accept: application/json`

*Deserialiser:* `JsonResponseDeserialiser` (Json.Net)
*ApiClient:* Wrapper for `System.Net.Http.HttpClient`
*Assertion:* `ExceptionAssertion` (can be replaced with `ConsoleAssertion`)


# Asserting Response

## As Dynamic Content

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.ReturnsDynamic(c => c.login == "defunkt", "Login did not match")
	.ReturnsDynamic(c => c.id == 2, "ID did not match")
	.Assert();
```

## As From Strongly Typed Model

*Model:*
``` C#
public class GitHubUser
{
	public int id { get; set; }
	public string login { get; set; }
}
```

*Test:*

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.Returns<GitHubUser>(c => c.login == "defunkt")
	.Returns<GitHubUser>(c => c.id == 2)
	.Assert();
```

## Asserting Header

*Test:*

``` C#
_configuration.Get("/users/defunkt")
	.Response()
	.Returns<GitHubUser>(c => c.login == "defunkt")
	.Returns<GitHubUser>(c => c.id == 2)
	.Assert();
```



## Failure Message Example 

*Test*

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



