using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Cipp.ClassCreation {
	
	public class AdvancedPropertyDrawerTemplates {

		[System.Serializable]
		public struct SubProperties {
			public string subPropertyName;
			public bool isSingleLine;
			public bool isArray;
			public bool hasChildren;
			public string hasPropertyDrawer;
			public float fixedChildrenHeight;
		}

		public static string[] customPropertyDrawerVariables {
			get {
				return new string[4] {
					"\n\tfloat width = 0;",
					"\n\tfloat x = 0;",
					"\n\tfloat y = 0;",
					"\n\tfloat PropertyHeight = 0;"
				};
			}
		}

		public const string OnGUIArguments = "Rect position, SerializedProperty property, GUIContent label";
		public const string OnGUICode = "\n\t\tlabel = EditorGUI.BeginProperty (position, label, property);\n"+
			"\n\t\tx = position.x;"+
			"\n\t\ty = position.y;"+
			"\n\t\twidth = position.y;\n" +
			"\n\t\tEditorGUI.indentLevel++;\n" +
			"\n\t\tstring PropertyName = property.displayName;\n" +
			"\n\t\tRect foldoutRect = DrawPropertyFoldout (PropertyName, property);" +
			"\n\t\tif (property.isExpanded) {" +
			"\n\t\t\tEditorGUI.indentLevel++;" +
			"\n\t\t\tDrawProperties (foldoutRect, property);" +
			"\n\t\t}\n" +
			"\n\t\tEditorGUI.EndProperty ();";

		public const string DrawPropertyFoldoutArguments = "string name, SerializedProperty property";
		public const string DrawPropertyFoldoutCode ="\n\t\tPropertyHeight = EditorGUIUtility.singleLineHeight;" +
			"\n\t\tRect tmpFoldoutRect = new Rect(x, y, width, EditorGUIUtility.singleLineHeight);" +
			"\n\t\tproperty.isExpanded = EditorGUI.Foldout (tmpFoldoutRect, property.isExpanded,new GUIContent(name));" +
			"\n\t\treturn tmpFoldoutRect;";


		public const string DrawPropertiesArguments = "Rect foldout, SerializedProperty property";
		public static string DrawPropertiesCode (SubProperties[] subPropertyReferences) {
			string tmp = "";

			string[] rectNames = new string[subPropertyReferences.Length];
			string[] rectEquals = new string[subPropertyReferences.Length];
			for (int i = 0; i < rectEquals.Length; i++) {
				rectNames[i] = "tmp_" + subPropertyReferences [i].subPropertyName + "Rect";
				if (i > 0) {
					rectEquals [i] = rectNames[i] + " = "+rectNames [i-1]+";";
				} else {
					rectEquals [i] = rectNames[i] + " = foldout;";
				}
			}
			for (int i = 0; i < rectEquals.Length; i++) {
				string aProperty = "\n\t\tRect " + rectEquals [i]+
					"\n\t\tif (property.FindPropertyRelative (\""+subPropertyReferences[i].subPropertyName+"\") != null) {";
				if (subPropertyReferences [i].isArray) {
					if (string.IsNullOrEmpty(subPropertyReferences [i].hasPropertyDrawer)) {
						aProperty += "\n\t\t\tPropertyHeight += EditorStatic.totalPropertyHeight;" +
							"\n\t\t\t" + rectNames [i] + ".y += EditorStatic.totalPropertyHeight;" +
							"\n\t\t\tSerializedProperty prop = property.FindPropertyRelative (\"" + subPropertyReferences [i].subPropertyName + "\");" +
							"\n\t\t\tEditorGUI.PropertyField ("+rectNames [i]+", prop, true);" +
							"\n\t\t\tPropertyHeight += EditorStatic.CalculatePropertyArrayHeight (prop);";
					} else {
						Debug.Log ("To Implement");
					}
				} else if (subPropertyReferences [i].hasChildren) {
					if (string.IsNullOrEmpty(subPropertyReferences [i].hasPropertyDrawer)) {
						aProperty += "\n\t\t\tPropertyHeight += EditorStatic.totalPropertyHeight;" +
						"\n\t\t\t" + rectNames [i] + ".y += EditorStatic.totalPropertyHeight;" +
						"\n\t\t\tSerializedProperty prop = property.FindPropertyRelative (\"" + subPropertyReferences [i].subPropertyName + "\");" +
						"\n\t\t\tEditorGUI.PropertyField (tmp_PositionsRect, prop, true);";
						if (subPropertyReferences [i].fixedChildrenHeight != 0) {
							aProperty += "\n\t\t\tPropertyHeight += " + subPropertyReferences [i].fixedChildrenHeight;
						} else {
							aProperty += "\n\t\t\tPropertyHeight += EditorStatic.totalPropertyHeight";
							Debug.Log ("There is a sub property with children but it doesn't have a fixed-height for children!");
						}
					} else {
						Debug.Log ("To Implement");
					}
				} else if (subPropertyReferences[i].isSingleLine) {
					aProperty += "\n\t\t\tPropertyHeight += EditorStatic.totalPropertyHeight;" +
					"\n\t\t\t" + rectNames [i] + ".y += EditorStatic.totalPropertyHeight;" +
					"\n\t\t\tEditorGUI.PropertyField (" + rectNames [i] + ", property.FindPropertyRelative (\"" + subPropertyReferences [i].subPropertyName + "\"));";
				}
				aProperty+= "\n\t\t}\n";
				tmp += aProperty;
			}

			return tmp;
		}

		public const string GetPropertyHeightArguments = "SerializedProperty property, GUIContent label";
		public const string GetPropertyHeightCode = "\n\t\tfloat tmp = base.GetPropertyHeight(property, label);" +
			"\n\t\ttmp += PropertyHeight;" +
			"\n\t\treturn tmp;";
	}
}
