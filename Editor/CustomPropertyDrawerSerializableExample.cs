using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Example3.SerializableExampleStruct))]
public class CustomPropertyDrawerSerializableExample : PropertyDrawer {

	float width = 0;
	float x = 0;
	float y = 0;

	float PropertyHeight = 0;

	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
		
		label = EditorGUI.BeginProperty (position, label, property);

		//Get Data
		x = position.x;
		y = position.y;
		width = position.width;

		//Increase Actual Indent Level
		EditorGUI.indentLevel++;

		//Property Name;
		string PropertyName = property.displayName;

		Rect foldoutRect = DrawPropertyFoldout (PropertyName, property);
		if (property.isExpanded) {
			EditorGUI.indentLevel++;
			DrawProperties (foldoutRect, property);
		}
		//End
		EditorGUI.EndProperty ();

	}

	Rect DrawPropertyFoldout (string name, SerializedProperty property) {
		PropertyHeight = EditorGUIUtility.singleLineHeight;
		Rect tmpFoldoutRect = new Rect(x, y, width, EditorGUIUtility.singleLineHeight);
		property.isExpanded = EditorGUI.Foldout (tmpFoldoutRect, property.isExpanded,new GUIContent(name));
		return tmpFoldoutRect;
	}

	void DrawProperties (Rect foldout, SerializedProperty property) {

		Rect tmp_movementDistanceRect = foldout;
		if (property.FindPropertyRelative ("movementDistance") != null) {
			PropertyHeight += EditorStatic.totalPropertyHeight;
			tmp_movementDistanceRect.y += EditorStatic.totalPropertyHeight;
			EditorGUI.PropertyField (tmp_movementDistanceRect, property.FindPropertyRelative ("movementDistance"));
		}

		Rect tmp_targetRect = tmp_movementDistanceRect;
		if (property.FindPropertyRelative ("target") != null) {
			PropertyHeight += EditorStatic.totalPropertyHeight;
			tmp_targetRect.y += EditorStatic.totalPropertyHeight;
			EditorGUI.PropertyField (tmp_targetRect, property.FindPropertyRelative ("target"));
		}

		Rect tmp_PositionsRect = tmp_targetRect;
		if (property.FindPropertyRelative ("positions") != null) {
			PropertyHeight += EditorStatic.totalPropertyHeight;
			tmp_PositionsRect.y += EditorStatic.totalPropertyHeight;
			SerializedProperty prop = property.FindPropertyRelative ("positions");
			EditorGUI.PropertyField (tmp_PositionsRect, prop, true);
			if (prop.isArray) {
				PropertyHeight += EditorStatic.CalculatePropertyArrayHeight (prop);
			}
		}
	}
		
	public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
		float tmp = base.GetPropertyHeight(property, label);
		tmp += PropertyHeight;
		return tmp;
	}
}
