//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//
//namespace Cipp.ClassCreation {
//
//	public class SerializedPropertyDebuggerTool : ScriptableWizard {
//
//		[System.Serializable]
//		public struct InspectedSerializedProperty {
//			public string serializedPropertyName;
//			public InspectedSerializedProperty[] childrenSerializedProperty;
//
//			public static void PrintThis (SerializedProperty serProp, InspectedSerializedProperty property) {
//				if (serProp.FindPropertyRelative (property.serializedPropertyName) != null) {
//					SerializedProperty foundProperty = serProp.FindPropertyRelative (property.serializedPropertyName);
//					Debug.Log ("Found: "+foundProperty.displayName+ "\nProperty Path: "+foundProperty.propertyPath);
//					for (int i = 0; i < property.childrenSerializedProperty.Length; i++) {
//						PrintThis (foundProperty, property.childrenSerializedProperty[i]);
//					}
//				}
//			}
//		}
//
//		[System.Serializable]
//		public struct InspectedHierarchy {
//			public string classType;
//			public InspectedSerializedProperty serializedProperty;
//
//			public static void PrintThis (SerializedObject serObj, InspectedHierarchy root) {
//				if (serObj.FindProperty (root.serializedProperty.serializedPropertyName) != null) {
//					SerializedProperty foundProperty = serObj.FindProperty (root.serializedProperty.serializedPropertyName);
//					Debug.Log ("Found: "+foundProperty.displayName + "\nProperty Path: "+foundProperty.propertyPath);
//					if (foundProperty.isArray) {
//						for (int i = 0; i < foundProperty.arraySize; i++) {
//							for (int ix = 0; ix < root.serializedProperty.childrenSerializedProperty.Length; ix++) {
//								InspectedSerializedProperty.PrintThis (foundProperty.GetArrayElementAtIndex (i), (root.serializedProperty.childrenSerializedProperty[ix]));
//							}
//						}
//					} else {
//						for (int i = 0; i < root.serializedProperty.childrenSerializedProperty.Length; i++) {
//							InspectedSerializedProperty.PrintThis (foundProperty, root.serializedProperty.childrenSerializedProperty [i]);
//						}
//					}
//				}
//			}
//		}
//
//		public InspectedHierarchy inspectedRoot;
//		public bool saveLogFile = true;
//		public GameObject target;
//
//		void OnEnable() {
//			if (Selection.activeGameObject != null) {
//				target = Selection.activeGameObject;
//			}
//		}
//
//
//		[MenuItem ("Tools/Class/Class Debugger Serialized Property")]
//		static void CreateWizard () {
//			ScriptableWizard.DisplayWizard<SerializedPropertyDebuggerTool>("Debug Target Class", "Debug");
//		}
//
//		void OnWizardCreate () {
//			if (target.GetComponent (inspectedRoot.classType)) {
//				SerializedObject serObj = new SerializedObject (target.GetComponent (inspectedRoot.classType));
//				if (serObj != null) {
//					InspectedHierarchy.PrintThis (serObj, inspectedRoot);
//				} else {
//					Debug.Log ("WFT?");
//				}
//			} else {
//				Debug.Log ("Component not found!");
//			}
//		}
//	}
//}
