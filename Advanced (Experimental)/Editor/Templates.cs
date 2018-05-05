using UnityEngine;
using UnityEditor;

namespace Cipp.ClassCreation {


	public static class Templates {

		public const string LogOfferedBy = "This log was offered by \"Cippman\"";
		public const string ClassOfferedBy ="//This class was offered by \"Cippman\"";
		public const string CSharpExtension = ".cs";

		public static string StringArrayToSingleString (string[] values, bool indempt = false) {
			string effectiveValue = "";
			for (int i = 0; i < values.Length; i++) {
				if (indempt) {
					effectiveValue += "\n" + values [i];
				} else {
					effectiveValue += values [i];
				}
			}
			return effectiveValue;
		}

		public static string VoidIncipit (string methodName, ReflectionUtilities.MethodProperties properties, string arguments = "") {
			if (string.IsNullOrEmpty (methodName)) {
				Debug.Log ("Passed method is null or empty!");
				return methodName;
			}
			string effectiveValue = "\n\t";
			if (properties.isPublic) {
				effectiveValue += "public ";
			}
			if (properties.isStatic) {
				effectiveValue += "static ";
			}
			else if (properties.isOverride) {
				effectiveValue += "override ";
			} else if (properties.isVirtual) {
				effectiveValue += "virtual ";
			}
			effectiveValue += "void "+methodName+" ("+arguments+") {\n";
			return effectiveValue;
		}

		public static string VoidComplete (string methodName, ReflectionUtilities.MethodProperties properties, string arguments = "", string code = "") {
			if (string.IsNullOrEmpty (methodName)) {
				Debug.Log ("Passed method is null or empty!");
				return methodName;
			}
			string effectiveValue = "\n\t";	
			if (properties.isPublic) {
				effectiveValue += "public ";
			}
			if (properties.isStatic) {
				effectiveValue += "static ";
			}
			else if (properties.isOverride) {
				effectiveValue += "override ";
			} else if (properties.isVirtual) {
				effectiveValue += "virtual ";
			}
			effectiveValue += "void "+methodName + " (" + arguments + ") {\n" + code + "\n\t}\n";
			return effectiveValue;
		}

		public static string MethodComplete (string methodName, ReflectionUtilities.MethodProperties properties, string methodType = "void", string arguments = "", string code = "") {
			if (string.IsNullOrEmpty (methodName)) {
				Debug.Log ("Passed method is null or empty!");
				return methodName;
			}
			string effectiveValue = "\n\t";	
			if (properties.isPublic) {
				effectiveValue += "public ";
			}
			if (properties.isStatic) {
				effectiveValue += "static ";
			}
			else if (properties.isOverride) {
				effectiveValue += "override ";
			} else if (properties.isVirtual) {
				effectiveValue += "virtual ";
			}
			effectiveValue += methodType+" " +methodName + " (" + arguments + ") {\n" + code + "\n\t}\n";
			return effectiveValue;
		}

		public static string EditorClassIncipit (string classType) {
			if (string.IsNullOrEmpty (classType)) {
				Debug.Log ("Passed class type is null or empty!");
				return classType;
			}
			string effectiveValue = "using UnityEngine;\nusing UnityEditor;\n\n" +
			                        "[CustomEditor(typeof(" + classType + "))]\n" +
			                        "public class " + classType + "Editor : Editor {\n";
			return effectiveValue;
		}

		public static string EditorOverrideOnInspectorGUI (string classType) {
			if (string.IsNullOrEmpty (classType)) {
				Debug.Log ("Passed class type is null or empty!");
				return classType;
			}
			string effectiveValue = "\n\tpublic override void OnInspectorGUI () {\n\n\t\t" +
			                        classType + " myScript = (" + classType + ")target;\n" +
			                        "\t\tDrawDefaultInspector();\n\n\t}";
			return effectiveValue;
		}

		public static string CustomPropertyDrawerClassIncipit(string serializableType) {
			if (string.IsNullOrEmpty (serializableType)) {
				Debug.Log ("Passed serializable type is null or empty!");
				return serializableType;
			}
			string effectivevalue = "using UnityEngine;\nusing UnityEditor;\n\n" +
			                        "[CustomPropertyDrawer(typeof(" + serializableType + "))]\n" +
			                        "public class " + serializableType + "PropertyDrawer : PropertyDrawer {\n";
			return effectivevalue;
		}
	}
}
