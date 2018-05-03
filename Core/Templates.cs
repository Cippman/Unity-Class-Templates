using System;
using System.Collections.Generic;
using CippSharp.ClassTemplates.Extensions;

namespace CippSharp.ClassTemplates
{
    public static class Templates
    {
        public const string Cippman = "\"Cippman\"";

        public static string ThingOffered(string thing, string by = Cippman)
        {
            return string.Format("//This {0} was offered by {1}.", thing, by);
        }
       
        public const string namespaceHere = "//Namespace Here";
        public const string codeHere = "//Code Here";
        public const string CSharpExtension = ".cs"; //CippSharp extension :P
        public const string tab = "\t";
        public const string carriageReturn = "\r";
        public const string lineFeed = "\n";
        public const string carriageReturnAndLineFeed = carriageReturn + lineFeed;
        public const string openBrace = "{";
        public const string carriageOpenBrace = carriageReturnAndLineFeed + openBrace;
        public const string closeBrace = "}";
        public const string carriageCloseBrace = carriageReturnAndLineFeed + closeBrace;
        public const string slash = "/";

        public static string StructIncipit(string valueType)
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
//            structLines.Add("using UnityEngine;");
//            structLines.Add(carriageReturnAndLineFeed);
//            structLines.Add(carriageReturnAndLineFeed);
//            structLines.Add(namespaceHere);
//            structLines.Add(carriageReturnAndLineFeed);
//            structLines.Add(string.Format("public struct {0}", valueType));
//            structLines.Add(carriageReturnAndLineFeed);
//            structLines.Add(openBrace);
//            structLines.Add(carriageReturnAndLineFeed);
//            structLines.Add(codeHere);
//            structLines.Add(carriageReturnAndLineFeed);
//            structLines.Add(closeBrace);
            return structLines;
        }

        public static void AddNamespace(List<string> lines, string _namespace)
        {
            List<string> tmpLines = new List<string>();
            int indexOfNamespace = -1;
            for (int i = 0; i < lines.Count; i++)
            {
                string currentLine = lines[i];
                if (currentLine == namespaceHere)
                {
                    indexOfNamespace = i;
                    tmpLines.Add(string.Format("namespace {0}", _namespace));
                    continue;
                }

                if (indexOfNamespace < 0)
                {
                    tmpLines.Add(currentLine);
                    continue;
                }

                if (i > indexOfNamespace)
                {
                    //Start Indent
                    //lines[]
                }
            }
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
        }
    }
}

