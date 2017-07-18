using System;
using System.Collections.Generic;
using System.Linq;
using restfluencing.Assertion;
using System.Text;

namespace restfluencing
{
	public class AssertionFailedException : Exception
	{
		public ExecutionResult Result { get; }

		public AssertionFailedException(string message) : base(message)
		{
		}

		public AssertionFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public AssertionFailedException(string message, ExecutionResult result) : base(message)
		{
			Result = result;
		}

		public AssertionFailedException(ExecutionResult result) : base(GetFullMessage(result))
		{
			Result = result;
		}

		public static string GetFullMessage(ExecutionResult result)
		{
			var msg = new StringBuilder();

			if (result.Results.Any())
			{
				msg
					.AppendLine("Failed with one or more assertions result.")
					.AppendLine()
					.AppendLine(GetErrorMessage(result.Results))
					.AppendLine();
			}

			var apiClientResponse = result.Response;
			if (apiClientResponse == null)
			{
				msg
					.AppendLine("It was not possible to complete the request to the server - no response was received.");
			}
			else
			{
				msg
					.AppendLine($"Response: {apiClientResponse.Status} {apiClientResponse.StatusCode}")
					.AppendLine(apiClientResponse.Content)
					.AppendLine();

				if (apiClientResponse.Headers.Count > 0)
				{
					msg
						.AppendLine("Headers");

					foreach (var header in apiClientResponse.Headers.Keys)
					{
						foreach (var value in apiClientResponse.Headers[header])
						{
							msg.AppendLine($"{header} : {value}");
						}
					}
				}
				else
				{
					msg
						.AppendLine("Response had no headers.");
				}
			}

			return msg.ToString();
		}

		public static string GetErrorMessage(IEnumerable<AssertionResult> results)
		{
			var error = new StringBuilder();
			foreach (var r in results)
			{
				error.AppendLine($"{r.CausedBy.RuleName}: {r.ErrorMessage}");
			}
			return error.ToString();
		}


	}
}