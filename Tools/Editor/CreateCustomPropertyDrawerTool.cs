using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using CippSharp.Reorderable;

namespace Cipp.ClassCreation {

	public class CreateCustomPropertyDrawerTool : ScriptableWizard {

		[System.Serializable]
		public class SubPropertiesArray : ReorderableArray<AdvancedPropertyDrawerTemplates.SubProperties>
		{
			public AdvancedPropertyDrawerTemplates.SubProperties[] Array
			{
				get { return array; }
			}
		}

		public string propertyDrawerType = "MySerializableType";
		
		[Reorderable] public SubPropertiesArray subProperties = new SubPropertiesArray();

		public string path = "Assets/Scripts/Editor/";

		[MenuItem ("Tools/Class/Create Property Drawer Class")]
		static void CreateWizard () 
		{
			ScriptableWizard.DisplayWizard<CreateCustomPropertyDrawerTool>("Create Property Drawer Class", "Create");
		}

		void OnWizardCreate ()
		{
			TemplateWriter.CreateFolder (path);
			string filename = propertyDrawerType;
			filename.Replace (".", "_");
			path += filename + "PropertyDrawer" + Templates.CSharpExtension;
			List<string> allLines = new List<string> ();
			allLines.Add (Templates.ClassOfferedBy);
			allLines.Add (Templates.CustomPropertyDrawerClassIncipit (propertyDrawerType));

			string PrivatePropertyDrawerVariables = "";
			foreach (string s in AdvancedPropertyDrawerTemplates.customPropertyDrawerVariables) {
				PrivatePropertyDrawerVariables += s;
			}

			allLines.Add (PrivatePropertyDrawerVariables);

			allLines.Add (Templates.VoidComplete("OnGUI", new ReflectionUtilities.MethodProperties(true,false,false,true), AdvancedPropertyDrawerTemplates.OnGUIArguments, AdvancedPropertyDrawerTemplates.OnGUICode));

			allLines.Add (Templates.MethodComplete("DrawPropertyFoldout", new ReflectionUtilities.MethodProperties(), "Rect", AdvancedPropertyDrawerTemplates.DrawPropertyFoldoutArguments, AdvancedPropertyDrawerTemplates.DrawPropertyFoldoutCode));

			allLines.Add (Templates.VoidComplete("DrawProperties", new ReflectionUtilities.MethodProperties(), AdvancedPropertyDrawerTemplates.DrawPropertiesArguments, AdvancedPropertyDrawerTemplates.DrawPropertiesCode(subProperties.Array)));

			allLines.Add (Templates.MethodComplete ("GetPropertyHeight", new ReflectionUtilities.MethodProperties (true, false, false, true), "float", AdvancedPropertyDrawerTemplates.GetPropertyHeightArguments, AdvancedPropertyDrawerTemplates.GetPropertyHeightCode));

			allLines.Add ("}");

			TemplateWriter.CreateClassFile (path, allLines.ToArray());
			Debug.Log ("Something its done! For the Wizard!");
		}
	}
}
