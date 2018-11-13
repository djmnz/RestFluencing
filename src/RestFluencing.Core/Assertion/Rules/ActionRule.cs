using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestFluencing.Assertion.Rules
{
    /// <summary>
    /// Rules that allows an Action to be called on the response object.
    /// This type of rule does not support asserts, but will fail if the response cannot be deserialised or if the Action throws an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ActionRule<T> : AssertionRule
    {
        private readonly Action<T> _action;
        private readonly Type _assertType;

        public ActionRule(Action<T> action)
            : base("Action")
        {
            _action = action;
            _assertType = typeof(T);
        }

        /// <summary>
        /// Calls the Action on the response object.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override IEnumerable<AssertionResult> Assert(AssertionContext context)
        {
            if (string.IsNullOrEmpty(context.Response.Content))
            {
                yield return new AssertionResult(this, $"Response was blank");
                yield break;
            }

            var obj = default(T);
            Exception error = null;
            try
            {
                obj = context.ResponseDeserialiser.GetResponse<T>(context);
            }
            catch (Exception ex)
            {
                error = ex;
            }

            if (obj == null)
                yield return new AssertionResult(this, $"Failed to deserialise response to {_assertType}");

            if (error != null)
                yield return new AssertionResult(this, error.Message);

            if (obj == null || error != null)
                yield break;

            AssertionResult result = null;

            try
            {
                _action(obj);
            }
            catch (Exception ex)
            {
                result = new AssertionResult(this, ex.ToString());
            }

            if (result != null)
                yield return result;
        }
    }
}
