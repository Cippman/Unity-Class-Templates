//This class was offered by "Cippman".
using UnityEngine;
using CippSharp.ClassTemplates.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public static partial class Creator/*vol. 4 custom property drawer creation*/
    {
    #if UNITY_EDITOR
        [MenuItem("Assets/Create/Class Templates/Property Drawer", false, 10)]
        public static void CreateEmptyPropertyDrawerClass()
        {
            PropertyDrawerCreatorPopup propertyDrawerCreatorPopup = PropertyDrawerCreatorPopup.OpenPopup();
            propertyDrawerCreatorPopup.onClosePopup += OnPropertyDrawerCreatorPopupCloseCall;
        }

        private static void OnPropertyDrawerCreatorPopupCloseCall(Popup popup)
        {
            PropertyDrawerCreatorPopup propertyDrawerCreatorPopup = (PropertyDrawerCreatorPopup) popup;
            if (propertyDrawerCreatorPopup.confirmed)
            {
                string directory = propertyDrawerCreatorPopup.selectedPath.Slash();
                string fileName = propertyDrawerCreatorPopup.typeInputString + Templates.CSharpExtension;
                string nam = propertyDrawerCreatorPopup.namespaceInputString;
                CreateEmptyPropertyDrawerClass(directory + fileName, propertyDrawerCreatorPopup.typeInputString, nam);
            }
        }

        public static void CreateEmptyPropertyDrawerClass(string fullPath, string classType, string classNamespace = "")
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
                ? AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "PropertyDrawer with Namespace")
                : AssetDatabaseUtility.GetAsset<TemplateObject>(t => t.templateName == "PropertyDrawer");
            
            if (templateObject == null)
            {
                Debug.LogError("No template object found for property drawer classes");
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
