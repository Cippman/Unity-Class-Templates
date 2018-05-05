using System;
using UnityEngine;
using CippSharp.ClassTemplates.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
	public static partial class Creator /*vol. 2 class creation*/
	{
#if UNITY_EDITOR
		[MenuItem("Assets/Create/Class Templates/Empty Struct", false, 10)]
		public static void CreateEmptyClass()
		{
			ClassCreatorPopup classCreatorPopup = ClassCreatorPopup.OpenPopup();
			classCreatorPopup.onClosePopup += OnClassCreatorPopupCloseCall;
		}

		public static void OnClassCreatorPopupCloseCall(Popup popup)
		{
			ClassCreatorPopup classCreatorPopup = (ClassCreatorPopup) popup;
			if (classCreatorPopup.confirmed)
			{
				string directory = classCreatorPopup.selectedPath.Slash();
				string fileName = classCreatorPopup.typeInputString + Templates.CSharpExtension;
				string nam = classCreatorPopup.namespaceInputString;
				string[] keywords = classCreatorPopup.classKeywords;
				CreateEmptyClass(directory + fileName, classCreatorPopup.typeInputString, nam, keywords);
			}
		}

		public static void CreateEmptyClass(string fullPath, string classType, string classNamespace = "", string[] classKeywords = null)
		{
			if (string.IsNullOrEmpty(fullPath))
			{
				Debug.LogError("Passed path is null or empty!");
				return;
			}

			if (string.IsNullOrEmpty(classType))
			{
				Debug.LogError("Passed structType is null or empty!");
				return;
			}
			
			bool hasNamespace = !string.IsNullOrEmpty(classNamespace);
			TemplateObject templateObject = (hasNamespace)
				? AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "EmptyClass with Namespace")
				: AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "EmptyClass");
			
			if (templateObject == null)
			{
				Debug.LogError("No template object found for structs");
				return;
			}
			
			string validWritableTemplate = (hasNamespace)
				? templateObject.template.Replace(Templates.placeholderType, classType)
				: templateObject.template.Replace(Templates.placeholderType, classType)
					.Replace(Templates.placeholderNamespace, classNamespace);

			bool hasKeywords = !classKeywords.IsNullOrEmpty();
			int lenght = (hasKeywords) ? classKeywords.Length : -1;
			
			if (lenght > 0)
			{
				if (lenght == 1)
				{
					validWritableTemplate = validWritableTemplate.Replace(Templates.placeholderClassKeywords, " "+classKeywords[0]+" ");
				}
				else
				{
					string replacement = string.Empty;
					for (int i = 0; i < lenght; i++)
					{
						if (i == 0)
						{
							replacement += " " + classKeywords[i];
						}
						else if (i == lenght - 1)
						{
							replacement += classKeywords[i] + " ";
						}
						else
						{
							replacement += " " + classKeywords[i] + " ";
						}
					}

					validWritableTemplate = validWritableTemplate.Replace(Templates.placeholderClassKeywords, replacement);
				}
			}
			else
			{
				validWritableTemplate = validWritableTemplate.Replace(Templates.placeholderClassKeywords, string.Empty);
			}
			
			Writer.CreateFile(fullPath, new[] {validWritableTemplate});
		}
#endif
	}
}