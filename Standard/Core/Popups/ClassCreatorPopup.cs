using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
#if UNITY_EDITOR
	public class ClassCreatorPopup : TwoChoicesPopup
	{
		public string namespaceInputString = "MyNamespace";
		public string typeInputString = "NewBehaviourScript";
		public string inheritance = "MonoBehaviour";
		public string[] classKeywords = new string[0];

		private SerializedObject serializedObject;
		private SerializedProperty ser_ClassKeywords;
		
		public static ClassCreatorPopup OpenPopup()
		{
			ClassCreatorPopup window = CreateInstance<ClassCreatorPopup>();
			window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 150);
			window.ShowPopup();
			return window;
		}

		protected override void GetReferences()
		{
			base.GetReferences();
			serializedObject = new SerializedObject(this);
			ser_ClassKeywords = serializedObject.FindProperty("classKeywords");
			
		}

		protected override void Draw()
		{
			base.Draw();
			GUILayout.Space(10);
			EditorGUILayout.LabelField("Insert a type and confirm!", EditorStyles.wordWrappedLabel);
			namespaceInputString = EditorGUILayout.TextField("Namespace", namespaceInputString);
			typeInputString = EditorGUILayout.TextField("Type", typeInputString);
			inheritance = EditorGUILayout.TextField("Inheritance", inheritance);
			serializedObject.Update();
			EditorGUILayout.PropertyField(ser_ClassKeywords, true);
			serializedObject.ApplyModifiedProperties();
			GUILayout.Space(50);
		}
	}
#endif
}