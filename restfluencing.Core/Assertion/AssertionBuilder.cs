using System;
using System.Collections.Generic;
using System.Linq;
using restfluencing.Assertion.Rules;

namespace restfluencing.Assertion
{
	public class AssertionBuilder
	{
		private IList<AssertionRule> _rules = new List<AssertionRule>();

		public IEnumerable<AssertionRule> Rules => _rules;

		public void AddRule(AssertionRule rule)
		{
			_rules.Add(rule);
		}

		public void OnlyOneRuleOf<T>(T rule) where T : AssertionRule
		{
			Array.ForEach(_rules.Where(r => r is T).ToArray(), r => _rules.Remove(r));
			_rules.Add(rule);
		}
	}
}