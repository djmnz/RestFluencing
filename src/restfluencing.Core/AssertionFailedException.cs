using System;
using System.Collections.Generic;
using System.Linq;
using RestFluencing.Assertion;
using System.Text;

namespace RestFluencing
{
	/// <summary>
	/// Exception raised when an Assertion rule failed.
	/// </summary>
	public class AssertionFailedException : Exception
	{
		/// <summary>
		/// Result of the assertion that raised this exception
		/// </summary>
		public ExecutionResult Result { get; }


		/// <summary>
		/// Constructor that includes a custom message to be appended with the results.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="result"></param>
		public AssertionFailedException(string message, ExecutionResult result) : base($"{message} {GetFullMessage(result)}")
		{
			Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		/// <summary>
		/// Constructor that generates a message for the result sent through.
		/// </summary>
		/// <param name="result"></param>
		public AssertionFailedException(ExecutionResult result) : base(GetFullMessage(result))
		{
			Result = result ?? throw new ArgumentNullException(nameof(result));
		}

		internal static string GetFullMessage(ExecutionResult result)
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

		internal static string GetErrorMessage(IEnumerable<AssertionResult> results)
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