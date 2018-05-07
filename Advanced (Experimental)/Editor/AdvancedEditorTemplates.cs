using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Cipp.ClassCreation {

	[System.Serializable]
	public struct SerializedPropertyRef {
		public string serializedPropertyDisplayName;
		public string serializedPropertyName;
		public string serializedProperty;
		public string completeSerializedProperty;
		public string findingSerializedProperty;
		public string drawingSerializedProperty;

		public SerializedPropertyRef (SerializedProperty property, bool useReorderableList) {
			if (!useReorderableList) {
				this.serializedPropertyDisplayName = property.displayName;
				this.serializedPropertyName = property.name;
				this.serializedProperty = "ser_" + this.serializedPropertyName;
				this.completeSerializedProperty = "SerializedProperty " + this.serializedProperty;
				
				this.findingSerializedProperty = "\t\tif (serializedObject.FindProperty (\"" + this.serializedPropertyName + "\") != null) {\n" +
				"\t\t\t" + this.serializedProperty + " = " + "serializedObject.FindProperty (\"" + this.serializedPropertyName + "\");\n" +
				"\t\t}\n";
				
				string tmpDrawingProp = "\t\tif (" + this.serializedProperty + " != null) {\n";
				if (property.isArray) {
					tmpDrawingProp += "\t\t\t" + this.serializedProperty + ".isExpanded = EditorGUILayout.Foldout (" + this.serializedProperty + ".isExpanded, " + this.serializedProperty + ".displayName);\n" +
					"\t\t\tif(" + this.serializedProperty + ".isExpanded) {\n" +
					"\t\t\t\t" + this.serializedProperty + ".arraySize = EditorGUILayout.IntField (\"Size\", " + this.serializedProperty + ".arraySize);\n" +
					"\t\t\t\tif(" + this.serializedProperty + ".arraySize > 0 ) {\n" +
					"\t\t\t\t\tfor(int i = 0; i < " + this.serializedProperty + ".arraySize; i++) {\n" +
					"\t\t\t\t\t\tvar item = " + this.serializedProperty + ".GetArrayElementAtIndex (i);\n" +
					"\t\t\t\t\t\tEditorGUILayout.PropertyField (item, true);\n" +
					"\t\t\t\t\t\tif ((item.hasChildren == true && item.isExpanded == true) || item.isExpanded == false) {\n" +
					"\t\t\t\t\t\t\tEditorGUILayout.BeginHorizontal ();\n" +
					"\t\t\t\t\t\t\tif (GUILayout.Button (\"v\")) {\n" +
					"\t\t\t\t\t\t\t\t" + this.serializedProperty + ".MoveArrayElement (i, i + 1);\n" +
					"\t\t\t\t\t\t\t\titem.isExpanded = false;\n" +
					"\t\t\t\t\t\t\t\t" + this.serializedProperty + ".GetArrayElementAtIndex (i + 1).isExpanded = true;\n" +
					"\t\t\t\t\t\t\t}\n" +
					"\t\t\t\t\t\t\tif (GUILayout.Button (\"^\")) {\n" +
					"\t\t\t\t\t\t\t\t" + this.serializedProperty + ".MoveArrayElement (i, i - 1);\n" +
					"\t\t\t\t\t\t\t\titem.isExpanded = false;\n" +
					"\t\t\t\t\t\t\t\t" + this.serializedProperty + ".GetArrayElementAtIndex (i - 1).isExpanded = true;\n" +
					"\t\t\t\t\t\t\t}\n" +
					"\t\t\t\t\t\t\tEditorGUILayout.EndHorizontal ();\n" +
					"\t\t\t\t\t\t\tEditorGUILayout.Space ();\n" +
					"\t\t\t\t\t\t\tEditorGUILayout.LabelField (\"Current Index: \" + i);\n" +
					"\t\t\t\t\t\t\tEditorGUILayout.BeginHorizontal ();\n" +
					"\t\t\t\t\t\t\tbool moveTo = false;\n" +
					"\t\t\t\t\t\t\tif (GUILayout.Button (\"Move To Index\")) {\n" +
					"\t\t\t\t\t\t\t\tmoveTo = true;\n" +
					"\t\t\t\t\t\t\t}\n" +
					"\t\t\t\t\t\t\ttaskIndex = EditorGUILayout.IntField (\"\", taskIndex);\n" +
					"\t\t\t\t\t\t\tif (moveTo) {\n" +
					"\t\t\t\t\t\t\t\t" + this.serializedProperty + ".MoveArrayElement (i, taskIndex);\n" +
					"\t\t\t\t\t\t\t\titem.isExpanded = false;\n" +
					"\t\t\t\t\t\t\t\t" + this.serializedProperty + ".GetArrayElementAtIndex (taskIndex).isExpanded = true;\n" +
					"\t\t\t\t\t\t\t}\n" +
					"\t\t\t\t\t\t\tEditorGUILayout.EndHorizontal ();\n" +
					"\t\t\t\t\t\t}\n" +
					"\t\t\t\t\t}\n" +
					"\t\t\t\t}\n" +
					"\t\t\t}\n";
				} else if (property.hasChildren) {
					tmpDrawingProp += "\t\t\tEditorGUILayout.PropertyField(" + this.serializedProperty + ", true);\n";
				} else {
					tmpDrawingProp += "\t\t\tEditorGUILayout.PropertyField(" + this.serializedProperty + ");\n";
				}
				tmpDrawingProp += "\t\t}\n";
				this.drawingSerializedProperty = tmpDrawingProp;
			} else {
				this.serializedPropertyDisplayName = property.displayName;
				this.serializedPropertyName = property.name;
				if (property.isArray) {
					this.serializedProperty = "reorderable_" + this.serializedPropertyName;
					this.completeSerializedProperty = "ReorderableList " + this.serializedProperty;

					this.findingSerializedProperty = "\t\tif (serializedObject.FindProperty (\"" + this.serializedPropertyName + "\") != null) {\n" +
						"\t\t\t" + this.serializedProperty + " = new ReorderableList (" + "serializedObject.FindProperty (\"" + this.serializedPropertyName + "\"));\n" +
						"\t\t}\n";
					
					this.drawingSerializedProperty = "\t\tif (" + this.serializedProperty + " != null) {\n"+
						"\t\t\t"+this.serializedProperty+".DoLayoutList();\n"+
						"\t\t}";

				} else {
					this.serializedProperty = "ser_" + this.serializedPropertyName;
					this.completeSerializedProperty = "SerializedProperty " + this.serializedProperty;
					
					this.findingSerializedProperty = "\t\tif (serializedObject.FindProperty (\"" + this.serializedPropertyName + "\") != null) {\n" +
						"\t\t\t" + this.serializedProperty + " = " + "serializedObject.FindProperty (\"" + this.serializedPropertyName + "\");\n" +
						"\t\t}\n";
					
					string tmpDrawingProp = "\t\tif (" + this.serializedProperty + " != null) {\n";
					if (property.hasChildren) {
						tmpDrawingProp += "\t\t\tEditorGUILayout.PropertyField(" + this.serializedProperty + ", true);\n";
					} else {
						tmpDrawingProp += "\t\t\tEditorGUILayout.PropertyField(" + this.serializedProperty + ");\n";
					}
					tmpDrawingProp += "\t\t}\n";
					this.drawingSerializedProperty = tmpDrawingProp;
				}
			}

		}
	}

	public class AdvancedEditorTemplates {

		public static List<SerializedPropertyRef> EditorClassAdvanced_SerializedProperties (object obj, bool useReorderableList) {
			List<SerializedPropertyRef> tmpSerializedProperties = new List<SerializedPropertyRef> ();
			if ((UnityEngine.Object)obj) {
				Debug.Log ("L'oggetto è compatibile con UnityEngine.Object");
			} else {
				Debug.Log ("Mi stai chiedendo di tirarti una sberla!");
				return tmpSerializedProperties;
			}
			
			List<PropertyInfoRef> propertyRefs = ReflectionUtilities.ClassDefaultProperties (obj);
			List<FieldInfoRef> fieldRefs = ReflectionUtilities.ClassDefaultFields (obj);
			SerializedObject serObj = new SerializedObject ((UnityEngine.Object)obj);

			for (int i = 0; i < propertyRefs.Count; i++) {
				if (serObj.FindProperty (propertyRefs [i].propertyInfoName) != null) {
					SerializedPropertyRef tmpSerPropRef = new SerializedPropertyRef (serObj.FindProperty (propertyRefs [i].propertyInfoName), useReorderableList); 
					if (!tmpSerializedProperties.Contains (tmpSerPropRef)) {
						tmpSerializedProperties.Add (tmpSerPropRef);
					}
				}
			}
			for (int i = 0; i < fieldRefs.Count; i++) {
				if (serObj.FindProperty (fieldRefs [i].fieldInfoName) != null) {
					SerializedPropertyRef tmpSerPropRef = new SerializedPropertyRef (serObj.FindProperty (fieldRefs [i].fieldInfoName), useReorderableList);
					if (!tmpSerializedProperties.Contains (tmpSerPropRef)) {
						tmpSerializedProperties.Add (tmpSerPropRef);
					}
				}
			}
			return tmpSerializedProperties;
		}
	}
}
