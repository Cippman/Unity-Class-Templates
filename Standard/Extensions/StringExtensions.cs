﻿using System;
using System.Collections.Generic;

namespace CippSharp.ClassTemplates.Extensions
{
    public static class StringExtensions
    {
        public enum AddMode
        {
            prefix,
            suffix
        }
        
        private const string tab = Templates.tab;
        private const string carriageReturnAndLineFeed = Templates.carriageReturnAndLineFeed;
        private const string slash = Templates.slash;
        
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

        /// <summary>
        /// Add tab to a string. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="addMode"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Tab(this string value, AddMode addMode = AddMode.prefix)
        {
            switch (addMode)
            {
                case AddMode.prefix:
                    return string.Concat(tab, value);
                case AddMode.suffix:
                    return string.Concat(value, tab);
                default:
                    throw new ArgumentOutOfRangeException("addMode", addMode, null);
            }
        }

        public static string AddPrefix(this string value, string prefix)
        {
            return string.Concat(prefix, value);
        }

        public static string AddSuffix(this string value, string suffix)
        {
            return string.Concat(value, suffix);
        }


        /// <summary>
        /// Sorround a string's list with some strings before and after.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public static List<string> SorroundWith (this List<string> values, IEnumerable<string> before = null, IEnumerable<string> after = null) 
        {
            List<string> sorroundedList = new List<string>();
            if (before != null)
            {
                sorroundedList.AddRange(before);
            }
            sorroundedList.AddRange(values);
            if (after != null)
            {
                sorroundedList.AddRange(after);
            }
            return sorroundedList;
        }
    }
}