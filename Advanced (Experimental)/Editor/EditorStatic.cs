using UnityEngine;
using UnityEditor;

public static class EditorStatic {
	//standard space dedicated to a property in the inspector window;
	public const float lineHeight = 18;
	//standard space between a property and another;
	public const float lineSpacing = 2;
	
	public static float totalPropertyHeight {
		get { return lineHeight + lineSpacing; }
	}
	
	//standard array height calculation  if it isn't a serialized property
	public static float CalculatePropertyArrayHeight (SerializedProperty arrayProperty) {
		float tmp = 0;
		if (arrayProperty.isExpanded) {
			tmp += totalPropertyHeight;
			if (arrayProperty.arraySize > 0) {
				tmp += totalPropertyHeight * arrayProperty.arraySize;
			}
		}
		return tmp;
	}
	//array height calculation if an array item has a PropertyDrawer
	public static float CalculatePropertyArrayHeight (SerializedProperty arrayProperty, PropertyDrawer itemDrawer, GUIContent label) {
		float tmp = 0;
		if (arrayProperty.isExpanded) {
			tmp += totalPropertyHeight;
			if (arrayProperty.arraySize > 0) {
				for (int i = 0; i < arrayProperty.arraySize; i++) {
					tmp += itemDrawer.GetPropertyHeight (arrayProperty.GetArrayElementAtIndex(i), label);
				}
			}
		}
		return tmp;
	}
	
}
