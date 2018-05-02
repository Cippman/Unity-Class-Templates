using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

namespace Cipp.ClassCreation {
	
	public class ClassDebuggerTool : ScriptableWizard {

		public string classType = "";
		public bool saveLogFile = true;
		public GameObject target = null;

		void OnEnable() {
			if (Selection.activeGameObject != null) {
				target = Selection.activeGameObject;
			}
		}

		[MenuItem ("Tools/Class/Class Debugger")]
		static void CreateWizard () {
			ScriptableWizard.DisplayWizard<ClassDebuggerTool>("Debug Target Class", "Debug");
		}

		void OnWizardUpdate () {
			if (target == null) {
				isValid = false;
			} else {
				isValid = true;
			}
		}

		void OnWizardCreate () {
			if (target.GetComponent (classType)) {
				List<FieldInfoRef> fieldValues = ReflectionUtilities.ClassDefaultFields (target.GetComponent (classType));
				List<PropertyInfoRef> propertyValues = ReflectionUtilities.ClassDefaultProperties (target.GetComponent(classType));
				List<MethodInfoRef> methods = ReflectionUtilities.ClassDefaultMethods (target.GetComponent(classType));

				if (saveLogFile) {
					List<string> allLines = new List<string> ();
					allLines.Add ("\nField Values of "+classType+":\n");
					foreach (FieldInfoRef fieldRef in fieldValues) {
						string val = string.Format ("\t{0} {1} = {2}\n", fieldRef.fieldInfoType, fieldRef.fieldInfoName, fieldRef.fieldInfoValue);
						if (!allLines.Contains(val)){
							allLines.Add (val);
						}
					}
					allLines.Add ("\nProperty Values of "+classType+":\n");
					foreach (PropertyInfoRef propertyRef in propertyValues) {
						string val = string.Format ("\t{0} {1} = {2}\n",propertyRef.propertyInfoType, propertyRef.propertyInfoName, propertyRef.propertyInfoValue);
						if (!allLines.Contains(val)){
							allLines.Add (val);
						}
					}

					allLines.Add ("\nMethods of "+classType+":\n");
					foreach (MethodInfoRef s in methods) {
						string val = "\t"+s.methodInfoVoid+"\n";
						if (!allLines.Contains(val)){
							allLines.Add (val);
						}
					}
					allLines.Add ("Log Complete!");
					TemplateWriter.CreateLogFile (target.name, allLines.ToArray());
				} else {
					foreach (FieldInfoRef fieldRef in fieldValues) {
						string val = "\t"+fieldRef.fieldInfoDeclaringType + " " + fieldRef.fieldInfoName + " = " + fieldRef.fieldInfoValue+"\n";
						Debug.Log (val);	
					}
					foreach (PropertyInfoRef propertyRef in propertyValues) {
						string val = "\t"+propertyRef.propertyDeclaringType+ " "+ propertyRef.propertyInfoName+ " = "+propertyRef.propertyInfoValue+"\n";
						Debug.Log (val);	
					}
					foreach (MethodInfoRef s in methods) {
						string val = "\t"+s.methodInfoVoid+"\n";
						Debug.Log (val);
					}
				}
			} else {
				Debug.Log ("Component not found!");
			}
		}
	}
	
}