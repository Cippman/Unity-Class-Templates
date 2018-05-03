using System;
using System.Collections.Generic;
using System.IO;
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

            string emptyStructTemplate = AssetDatabaseUtility.GetAssetPath("EmptyStructTemplate", "l:ClassTemplate");
       
            if (string.IsNullOrEmpty(emptyStructTemplate))
            {
                return;
            }

            List<string> linesToWrite = new List<string>();
            linesToWrite.Add(Templates.ThingOffered("struct"));
            
            string[] allFileLines = File.ReadAllLines(emptyStructTemplate);
            
            for (int i = 0; i < allFileLines.Length; i++)
            {
                if (allFileLines[i].Contains("<Type>"))
                {
                    allFileLines[i] = allFileLines[i].Replace("<Type>", type);
                }

                linesToWrite.Add(allFileLines[i]);
            }

            Writer.CreateFile(fullPath, linesToWrite.ToArray());
        }

#if UNITY_EDITOR
        public static void CreateEmptyClass()
        {
            
        }
#endif
    }
}