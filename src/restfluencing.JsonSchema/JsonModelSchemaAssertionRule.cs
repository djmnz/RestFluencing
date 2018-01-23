using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using RestFluencing.Assertion;
using RestFluencing.Assertion.Rules;

namespace RestFluencing.JsonSchema
{
	/// <summary>
	/// Assertion rule that uses JsonSchema to validate the response body.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class JsonModelSchemaAssertionRule<T> : AssertionRule
	{
		private readonly JSchema schema;

		/// <summary>
		/// 
		/// </summary>
		public JsonModelSchemaAssertionRule() : base("ModelSchema")
		{
			schema = JsonSchemaProvider.Instance.GetSchema<T>();
		}

		/// <summary>
		/// Specified generator
		/// </summary>
		/// <param name="generator"></param>
		public JsonModelSchemaAssertionRule(JSchemaGenerator generator) : base("ModelSchema")
		{
			schema = generator.Generate(typeof(T));
		}


		/// <summary>
		/// Asserts the response against the schema object type and generator.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override IEnumerable<AssertionResult> Assert(AssertionContext context)
		{
			JToken obj = JToken.Parse(context.Response.Content);
			IList<string> messages;

			if (!obj.IsValid(schema, out messages))
			{
				foreach (var m in messages)
				{
					yield return new AssertionResult(this, m);
				}
			}
		}
	}
}