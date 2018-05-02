using System;
using NUnit.Framework.Internal;
using UnityEngine;

namespace CippSharp.ClassTemplates.Extensions
{
    public static class StringExtensions
    {
        public const string carriageReturn = "\r";
        public const string lineFeed = "\n";
        public const string carriageReturnAndLineFeed = carriageReturn + lineFeed;
        public const string slash = "/";
        
        /// <summary>
        /// Slash-ize the end of a string. :D
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Slash(this string value)
        {
            return !value.EndsWith(slash) ? string.Concat(value, slash) : value;
        }

        
        /// <summary>
        /// Add a carriage return with line feed at the end of the string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Return(this string value)
        {
            return string.Concat(value, carriageReturnAndLineFeed);
        }
    }
}