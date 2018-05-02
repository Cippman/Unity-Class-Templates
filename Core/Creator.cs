using System.Collections.Generic;
using CippSharp.ClassTemplates.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public static class Creator
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Create/Class Templates/Empty Struct", false, 10)]
        [MenuItem("ROBE/Class Templates/Empty Struct", false, 10)]
        public static void CreateEmptyStruct()
        {
            EmptyStructPopup emptyStructPopup = EmptyStructPopup.OpenPopup();
            emptyStructPopup.onClosePopup += OnEmptyStructPopupCloseCall;
        }

        public static void OnEmptyStructPopupCloseCall(EmptyStructPopup popup)
        {
            if (popup.closeConfirmed)
            {
                string directory = popup.defaultDirectoryPath.Slash();
                string fileName = popup.typeInputString + Templates.CSharpExtension;
                CreateEmptyStruct(directory + fileName, popup.typeInputString);
            }
            else
            {
                CreateEmptyStruct(string.Empty, string.Empty);
            }
        }
#endif

        public static void CreateEmptyStruct(string fullPath, string type, string _namespace = "")
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                return;
            }
            
            if (string.IsNullOrEmpty(type))
            {
                return;
            }
            
            List<string> lines = new List<string>();
            lines.Add(Templates.ThingOffered("struct"));
            lines.Add(Templates.StructIncipit(type));
            lines.Add(Templates.closeBrace);
            Writer.CreateFile(fullPath, lines.ToArray());
        }
    }
}