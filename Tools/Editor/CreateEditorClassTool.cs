using UnityEngine;
using UnityEditor;

namespace Cipp.ClassCreation {

	public class CreateEditorClassTool : ScriptableWizard {

		public string classType = "MyClass";
		public string path = "Assets/Scripts/Editor/";

		[MenuItem ("Tools/Class/Create Editor Class")]
		static void CreateWizard () {
			ScriptableWizard.DisplayWizard<CreateEditorClassTool>("Create Editor Class", "Create");
		}

		void OnWizardCreate () {
			TemplateWriter.CreateFolder (path);
			path += classType + "Editor" + Templates.CSharpExtension;
			string[] classBody = new string[4];
			classBody [0] = Templates.ClassOfferedBy;
			classBody [1] = Templates.EditorClassIncipit (classType);
			classBody [2] = Templates.EditorOverrideOnInspectorGUI (classType);
			classBody [3] = "}";
			TemplateWriter.CreateClassFile (path, classBody);
			Debug.Log ("Something its done! For the Wizard!");
		}
	}
}
