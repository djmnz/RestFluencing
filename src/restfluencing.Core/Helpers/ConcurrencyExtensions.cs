using System.Threading.Tasks;

namespace RestFluencing.Helpers
{
	internal static class ConcurrencyExtensions
	{
		public static T GetSyncResult<T>(this Task<T> task)
		{
			return task.GetAwaiter().GetResult();
		}
	}
}