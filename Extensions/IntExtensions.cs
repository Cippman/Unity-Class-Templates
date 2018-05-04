using UnityEngine;
using System.Collections.Generic;

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

		public static bool IsValidIndex<T>(this int index, T[] array)
		{
			return index >= 0 && index < array.Length;
		}
		
		public static bool IsValidIndex<T>(this int index, List<T> list)
		{
			return index >= 0 && index < list.Count;
		}
	}
}
