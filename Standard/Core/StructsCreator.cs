using UnityEngine;
using CippSharp.ClassTemplates.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public static partial class Creator/*vol. 1 struct creation*/ 
    {
        
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Class Templates/Empty Struct", false, 10)]
        public static void CreateEmptyStruct()
        {
            StructCreatorPopup structCreatorPopup = StructCreatorPopup.OpenPopup();
            structCreatorPopup.onClosePopup += OnStructCreatorPopupCloseCall;
        }

        public static void OnStructCreatorPopupCloseCall(Popup popup)
        {
            StructCreatorPopup structCreatorPopup = (StructCreatorPopup) popup;
            if (structCreatorPopup.confirmed)
            {
                string directory = structCreatorPopup.selectedPath.Slash();
                string fileName = structCreatorPopup.typeInputString + Templates.CSharpExtension;
                string nam = structCreatorPopup.namespaceInputString;
                CreateEmptyStruct(directory + fileName, structCreatorPopup.typeInputString, nam);
            }
        }

        public static void CreateEmptyStruct(string fullPath, string structType, string structNamespace = "")
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                Debug.LogError("Passed path is null or empty!");
                return;
            }

            if (string.IsNullOrEmpty(structType))
            {
                Debug.LogError("Passed structType is null or empty!");
                return;
            }

            bool hasNamespace = !string.IsNullOrEmpty(structNamespace);
            TemplateObject templateObject = (hasNamespace)
                ? AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "EmptyStruct with Namespace")
                : AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "EmptyStruct");

            if (templateObject == null)
            {
                Debug.LogError("No template object found for structs");
                return;
            }

            string validWritableTemplate = (hasNamespace)
                ? templateObject.template.Replace(Templates.placeholderType, structType)
                : templateObject.template.Replace(Templates.placeholderType, structType)
                    .Replace(Templates.placeholderNamespace, structNamespace);

            Writer.CreateFile(fullPath, new[] {validWritableTemplate});
        }
#endif
        
    }
}