//This class was offered by "Cippman"
using UnityEditor;
using CippSharpEditor.Reorderable;

[CustomEditor(typeof(Example1))]
public class Example1Editor : Editor {

	SerializedProperty ser_moventDistance;

	SerializedProperty ser_target;

	ReorderableList reorderable_positions;


	public override void OnInspectorGUI () {

		Example1 myScript = (Example1)target;

		GetProperties();

		serializedObject.Update();

		DrawProperties();

		serializedObject.ApplyModifiedProperties();

	}


	void GetProperties () {

		if (serializedObject.FindProperty ("moventDistance") != null) {
			ser_moventDistance = serializedObject.FindProperty ("moventDistance");
		}

		if (serializedObject.FindProperty ("target") != null) {
			ser_target = serializedObject.FindProperty ("target");
		}

		if (serializedObject.FindProperty ("positions") != null) {
			reorderable_positions = new ReorderableList (serializedObject.FindProperty ("positions"));
		}

	}

	int taskIndex = 0;

	void DrawProperties () {

		if (ser_moventDistance != null) {
			EditorGUILayout.PropertyField(ser_moventDistance);
		}

		if (ser_target != null) {
			EditorGUILayout.PropertyField(ser_target, true);
		}

		if (reorderable_positions != null) {
			reorderable_positions.DoLayoutList();
		}
	}

}
