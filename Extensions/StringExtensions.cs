using System;
using UnityEngine;

namespace CippSharp.ClassTemplates.Extensions
{
    public static class StringExtensions
    {
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
    }
}