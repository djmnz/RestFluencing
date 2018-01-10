using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using restfluencing.Assertion;
using restfluencing.Assertion.Rules;

namespace restfluencing.JsonSchema
{
	public class JsonModelSchemaAssertionRule<T> : AssertionRule
	{
		private readonly JSchema schema;
		public JsonModelSchemaAssertionRule() : base("ModelSchema")
		{
			schema = JsonSchemaProvider.Instance.GetSchema<T>();
		}

		public JsonModelSchemaAssertionRule(JSchemaGenerator generator) : base("ModelSchema")
		{
			schema = generator.Generate(typeof(T));
		}

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