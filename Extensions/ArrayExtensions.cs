using UnityEngine;

namespace CippSharp.ClassTemplates
{
	public static class ArrayExtensions 
	{
		/// <summary>
		/// Return if an array is null or empty
		/// </summary>
		/// <param name="array"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool IsNullOrEmpty<T>(this T[] array)
		{
			return array == null || array.Length < 1;
		}
	}
}