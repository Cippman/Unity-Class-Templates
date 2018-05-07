using UnityEngine;
using CippSharp.ClassTemplates.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public static partial class Creator /*vol. 3 custom editor creation*/
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Class Templates/Editor Class", false, 10)]
        public static void CreateEmptyEditorClass()
        {
            EditorClassCreatorPopup editorClassCreatorPopup = EditorClassCreatorPopup.OpenPopup();
            editorClassCreatorPopup.onClosePopup += OnEditorCreatorPopupCloseCall;
            
        }

        private static void OnEditorCreatorPopupCloseCall(Popup popup)
        {
            EditorClassCreatorPopup editorClassCreatorPopup = (EditorClassCreatorPopup) popup;
            if (editorClassCreatorPopup.confirmed)
            {
                string directory = editorClassCreatorPopup.selectedPath.Slash();
                string fileName = editorClassCreatorPopup.typeInputString + Templates.CSharpExtension;
                string nam = editorClassCreatorPopup.namespaceInputString;
                CreateEmptyEditorClass(directory + fileName, editorClassCreatorPopup.typeInputString, nam);
            }
        }

        public static void CreateEmptyEditorClass(string fullPath, string classType, string classNamespace = "")
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                Debug.LogError("Passed path is null or empty!");
                return;
            }

            if (string.IsNullOrEmpty(classType))
            {
                Debug.LogError("Passed classType is null or empty!");
                return;
            }
            
            bool hasNamespace = !string.IsNullOrEmpty(classNamespace);
            TemplateObject templateObject = (hasNamespace)
                ? AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "EditorClass with Namespace")
                : AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "EditorClass");
            
            if (templateObject == null)
            {
                Debug.LogError("No template object found for editor classes");
                return;
            }
            
            string validWritableTemplate = (hasNamespace)
                ? templateObject.template.Replace(Templates.placeholderType, classType)
                    .Replace(Templates.placeholderNamespace, classNamespace)
                : templateObject.template.Replace(Templates.placeholderType, classType);
           
            Writer.CreateFile(fullPath, new[] {Templates.CippSponsor(Templates.classKeyword), validWritableTemplate});
        }
#endif
    }
}