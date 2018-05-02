using System;
using UnityEngine;

namespace CippSharp.ClassTemplates.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Determines if an enum has the given flag defined bitwise.
        /// Fallback equivalent to .NET's Enum.HasFlag().
        /// </summary>
        public static bool HasFlag(this Enum value, Enum flag)
        {
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

    }
}
