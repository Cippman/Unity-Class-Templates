using System;

namespace CippSharp.ClassTemplates
{
    public static class Templates
    {
        public const string Cippman = "\"Cippman\"";

        public static string ThingOffered(string thing, string by = Cippman)
        {
            return string.Format("//This {0} was offered by {1}.", thing, by);
        }

        //Placeholders
        public const string placeholderType = "<Type>";
        public const string placeholderNamespace = "<Namespace>";
        //Keyword names
        public const string namespaceKeyword = "namespace";
        
        public const string CSharpExtension = ".cs"; //CippSharp extension :P
        
        //String utilities
        public const string tab = "\t";
        public const string carriageReturn = "\r";
        public const string lineFeed = "\n";
        public const string carriageReturnAndLineFeed = carriageReturn + lineFeed;
        public const string openBrace = "{";
        public const string carriageOpenBrace = carriageReturnAndLineFeed + openBrace;
        public const string closeBrace = "}";
        public const string carriageCloseBrace = carriageReturnAndLineFeed + closeBrace;
        public const string slash = "/";

        /*public static string StructIncipit(string valueType)
        {
            if (string.IsNullOrEmpty(valueType))
            {
                throw new NullReferenceException();
            }

            string s = string.Format("using UnityEngine;" + carriageReturnAndLineFeed + carriageReturnAndLineFeed +
                                     "public struct {0} {1}"+ carriageReturnAndLineFeed, valueType, openBrace.Return());
            return s;
        }

        public static List<string> Struct(string valueType)
        {
            if (string.IsNullOrEmpty(valueType))
            {
                throw new NullReferenceException();
            }
            
            
            List<string> structLines = new List<string>();
            return structLines;
        }

        public static string ClassIncipit(string classType)
        {
            if (string.IsNullOrEmpty(classType))
            {
                throw new NullReferenceException();
            }

            string s = string.Format("using UnityEngine;" + carriageReturnAndLineFeed + carriageReturnAndLineFeed +
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
        }*/
    }
}

