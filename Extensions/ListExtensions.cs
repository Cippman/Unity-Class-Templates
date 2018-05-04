using System.Collections.Generic;

namespace CippSharp.ClassTemplates.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns if given list is null or empty.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count < 1;
        }
    }
}