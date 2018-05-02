using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Cipp.ClassCreation {

	public class CreateEditorClassAdvancedTool : ScriptableWizard {

		public string classType = "MyClass";
		public string path = "Assets/Scripts/Editor/";
		public bool useReorderableListForArrays = true;
		public GameObject target = null;

		void OnEnable() {
			if (Selection.activeGameObject != null) {
				target = Selection.activeGameObject;
			}
		}

		[MenuItem ("Tools/Class/Create Editor Class Advanced")]
		static void CreateWizard () {
			ScriptableWizard.DisplayWizard<CreateEditorClassAdvancedTool>("Create Editor Class", "Create");
		}

		void OnWizardUpdate () {
			if (target == null) {
				isValid = false;
			} else {
				isValid = true;
			}
		}

		void OnWizardCreate () {
			if (target.GetComponent(classType)) {
				TemplateWriter.CreateFolder (path);
				path += classType + "Editor" + Templates.CSharpExtension;

				List<SerializedPropertyRef> serProperties = AdvancedEditorTemplates.EditorClassAdvanced_SerializedProperties (target.GetComponent(classType), useReorderableListForArrays);

				List<string> allLines = new List<string> ();
				allLines.Add (Templates.ClassOfferedBy);
				if (useReorderableListForArrays) {
					allLines.Add ("using CDF.Editor;");
				}
				allLines.Add (Templates.EditorClassIncipit (classType));

				for (int i =0; i < serProperties.Count; i++) {
					string val = string.Format ("\t{0};\n", serProperties [i].completeSerializedProperty);
					if (!allLines.Contains (val)) {
						allLines.Add (val);
					}
				}

				string OnInspectorGUICode = ("\n\t\t"+classType+" myScript = ("+classType+")target;\n") +
											("\n\t\tGetProperties();\n") +
				                            ("\n\t\tserializedObject.Update();\n") +
				                            ("\n\t\tDrawProperties();\n") +
				                            ("\n\t\tserializedObject.ApplyModifiedProperties();\n");
				allLines.Add (Templates.VoidComplete("OnInspectorGUI", new ReflectionUtilities.MethodProperties (true, false, false, true), "", OnInspectorGUICode));

				string GetPropertiesCode = "";
				for (int i = 0; i < serProperties.Count; i++) {
					GetPropertiesCode += string.Format("\n{0}", serProperties [i].findingSerializedProperty);
				}
				allLines.Add (Templates.VoidComplete ("GetProperties", new ReflectionUtilities.MethodProperties(), "", GetPropertiesCode));

				allLines.Add ("\tint taskIndex = 0;");
				string DrawPropertiesCode = "";
				for (int i = 0; i < serProperties.Count; i++) {
					DrawPropertiesCode += string.Format("\n{0}", serProperties [i].drawingSerializedProperty);
				}
				allLines.Add (Templates.VoidComplete("DrawProperties", new ReflectionUtilities.MethodProperties(), "", DrawPropertiesCode));
				allLines.Add ("}");
				TemplateWriter.CreateClassFile (path, allLines.ToArray());
				Debug.Log ("Something its done! For the Wizard!");
			} else {
				Debug.Log ("Component not found!");
			}
		}
	}
}