//using UnityEngine;
//using UnityEditor;
//using System.IO;
//
//public class WizardCreateClass : ScriptableWizard {
//
//	public string className = "NewEmptyClass";
//	public string path = "Assets/Scripts/";
//
//	[HideInInspector]public string customExtension = ".txt";
//	[HideInInspector]public string customDerivation = "CustomClassName";
//	[HideInInspector]public string[] customUsings = new string[2] {"using Unity.Engine;", "using System.Collections;"};
//
//	public enum Code
//	{
//		csharp = 0,
//		custom = 2
//	}
//	public enum ClassDerivation {
//		mono = 0,
//		editor = 1,
//		custom = 2
//	}
//	public enum ClassSetup
//	{
//		standard = 0,
//		triggerVoids = 1,
//		collisionVoids = 2,
//		custom = 3
//	}
//
//	public Code basicCode = Code.csharp;
//	public ClassDerivation basicDerivation = ClassDerivation.mono; 
//	public ClassSetup basicTemplates = ClassSetup.standard;
//
//
//	//TODO: if custom
//	public WritableContent[] customTemplate;
//
//	private FileStream filestream;
//	private StreamWriter streamwriter;
//	private TextWriter textwriter;
//
//	private string[] extensions = new string[2] {".cs",	".js"};
//
//	[MenuItem ("Custom/Create Script Class")]
//	static void CreateWizard () {
//		ScriptableWizard.DisplayWizard<WizardCreateClass>("Create Class", "For the Wizard and his magic!");
//	}
//
//	void OnWizardUpdate () {
//		helpString = "si puoi davvero farlo, creare una classe proceduralmente!";
//		isValid = true;
//	}
//
//
//	void OnWizardCreate () {
//		string tmpPath = path;
//		if (basicDerivation == ClassDerivation.editor) {
//			editorVoidTemplate = "\npublic override void OnInspectorGUI () {" +
//				"\n" +
//				"\n" + className + " myScript = ("+className+")target;"+
//				"\n" + "DrawDefaultInspector();" +
//				"\n}";
//			tmpPath += "Editor/";
//		}
//		tmpPath += className;
//		if ((int)basicCode < 2) {
//			tmpPath += extensions [(int)basicCode];
//		} else {
//			tmpPath += customExtension;
//		}
//
//		filestream = new FileStream (tmpPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
//		streamwriter = new StreamWriter (filestream);
//
////		if (filestream.CanWrite) {
////			Debug.Log ("Posso Scrivere");
////			WriteClassBody ();
////		}
////			
//		AssetDatabase.ImportAsset (tmpPath);
//	}
//
//
//	void WriteClassRequirements () {
//
//		switch (basicDerivation) {
//		case ClassDerivation.mono:
//			textwriter.WriteLine ("//The very first line!");
//			textwriter.WriteLine ("using UnityEngine; \nusing System.Collections;");
//			textwriter.WriteLine ("");
//			textwriter.WriteLine ("public class " + className + " : " + "MonoBehaviour" + " { " );
//			textwriter.WriteLine ("");
//			break;
//		case ClassDerivation.editor:
//			textwriter.WriteLine ("//The very first line!");
//			textwriter.WriteLine ("using UnityEngine; \nusing Unity.Editor;");
//			textwriter.WriteLine ("");
//			textwriter.WriteLine ("[CustomEditor(typeof("+className+")]");
//			textwriter.WriteLine ("public class " + className+"Editor" + " : " + "Editor" + " { " );
//			textwriter.WriteLine ("");
//			break;
//		case ClassDerivation.custom:
//			textwriter.WriteLine ("//The very first line!");
//			foreach (string us in customUsings) {
//				textwriter.WriteLine ("\n"+us);
//			}
//			textwriter.WriteLine ("");
//			textwriter.WriteLine ("public class " + className + " : " + customDerivation + " { " );
//			textwriter.WriteLine ("");
//			break;
//		}
//	}
//
//
//	void WriteClassBody () {
//		textwriter = streamwriter;
//		//writing the basics
//		WriteClassRequirements();
//
//		//class body
//		if (basicDerivation != ClassDerivation.editor) {
//			switch (basicTemplates) {
//			case ClassSetup.standard:
//				textwriter.WriteLine (standardTemplate);
//				break;
//			case ClassSetup.triggerVoids:
//				textwriter.WriteLine (triggerVoids);
//				break;
//			case ClassSetup.collisionVoids:
//				textwriter.WriteLine (collisionVoids);
//				break;
//			case ClassSetup.custom:
//				foreach (WritableContent wc in customTemplate) {
//					textwriter.WriteLine (wc.customBeforeTemplate);
//					//textwriter.WriteLine (wc.fileTxT.ReadToEnd());
//					textwriter.WriteLine (wc.customAfterTemplate);
//				}
//				Debug.Log ("Pernacchia");
//				break;
//			}
//		} else if (basicDerivation == ClassDerivation.editor) {
//			textwriter.WriteLine (editorVoidTemplate);
//		}
//		textwriter.WriteLine ("}");
//		textwriter.WriteLine ("\n//This class was offered by ''Cippman''");
//		textwriter.Close ();
//
//		Debug.Log ("Scritto Finito");
//	}
//}
