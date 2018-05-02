using System;
using CippSharp.ClassTemplates.Extensions;
using UnityEngine;

namespace CippSharp.ClassTemplates
{
    public static class Templates
    {
        public const string Cippman = "\"Cippman\"";

        public static string ThingOffered(string thing, string by = Cippman)
        {
            return string.Format("//This {0} was offered by {1}.", thing, by);
        }

        public const string CSharpExtension = ".cs"; //CippSharp extension :P
        public const string tab = "\t";
        public const string carriageReturn = "\r";
        public const string lineFeed = "\n";
        public const string carriageReturnAndLineFeed = carriageReturn + lineFeed;
        public const string openBrace = "{";
        public const string closeBrace = "}";

        public static string StructIncipit(string valueType)
        {
            if (string.IsNullOrEmpty(valueType))
            {
                throw new NullReferenceException();
            }

            string s = string.Format("using UnityEngine;" + carriageReturnAndLineFeed +
                                     "public struct {0} {1}"+ carriageReturnAndLineFeed, valueType, openBrace.Return());
            return s;
        }

        public static string ClassIncipit(string classType)
        {
            if (string.IsNullOrEmpty(classType))
            {
                throw new NullReferenceException();
            }

            string s = string.Format("using UnityEngine;" + carriageReturnAndLineFeed +
                                     "public class {0} {1}"+ carriageReturnAndLineFeed, classType, openBrace.Return());
            return s;
        }

        public static string EditorClassIncipit(string classType)
        {
            if (string.IsNullOrEmpty(classType))
            {
                throw new NullReferenceException();
            }

            string s = string.Format("using UnityEngine;" + carriageReturnAndLineFeed +
                                     "using UnityEditor;" + carriageReturnAndLineFeed + carriageReturnAndLineFeed +
                                     "[CustomEditor(typeof({0})]" + carriageReturnAndLineFeed +
                                     "public class {0}Editor : Editor {1}" + carriageReturnAndLineFeed, classType, openBrace.Return());
            return s;
        }

        public static string CustomPropertyDrawerIncipit(string serializableType)
        {
            if (string.IsNullOrEmpty(serializableType))
            {
                throw new NullReferenceException();
            }

            string s = string.Format("using UnityEngine;"+ carriageReturnAndLineFeed +
                                     "using UnityEditor;"+ carriageReturnAndLineFeed + carriageReturnAndLineFeed +
                                     "[CustomPropertyDrawer(typeof({0}))]"+ carriageReturnAndLineFeed +
                                     "public class {0}PropertyDrawer : PropertyDrawer {1}"+ carriageReturnAndLineFeed, serializableType, openBrace.Return());
            return s;
        }
    }
}

