using UnityEngine;

namespace CippSharp.ClassTemplates
{
	public static class IntExtensions
	{
		/// <summary>
		/// Returns if an int isOdd
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool IsOdd(this int value)
		{
			return value % 2 != 0;
		}
	}
}
