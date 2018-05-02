//This class was offered by "Cippman"
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Example2))]
public class Example2Editor : Editor {


	public override void OnInspectorGUI () {

		Example2 myScript = (Example2)target;
		DrawDefaultInspector();

	}
}
