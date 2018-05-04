using System.IO;
using System.Collections.Generic;
using CippSharp.ClassTemplates.Extensions;
using UnityEngine;

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

            string emptyStructTemplateFilePath = AssetDatabaseUtility.GetAssetPath("EmptyStructTemplate", "l:ClassTemplate");
            
            if (string.IsNullOrEmpty(emptyStructTemplateFilePath))
            {
                Debug.LogError("Missing a EmptyStructTemplate file in project. And ensure that it's labeled \"ClassTemplate\"");
                return;
            }

            bool hasNamespace = !string.IsNullOrEmpty(structNamespace);
            List<string> writableLines = new List<string>();
            string[] s = emptyStructTemplateFilePath.Split(new [] {'\\'});
           
            writableLines.Add(Templates.ThingOffered("struct"));
            
            string[] templateFileLines = File.ReadAllLines(emptyStructTemplateFilePath);
            
            for (int i = 0; i < templateFileLines.Length; i++)
            {
                string currentLine = templateFileLines[i];
                if (currentLine.Contains(Templates.placeholderType))
                {
                    currentLine = currentLine.Replace(Templates.placeholderType, structType);
                }

                if (hasNamespace)
                {
                    currentLine = Templates.tab + currentLine;
                }

                writableLines.Add(currentLine);
            }

            if (hasNamespace)
            {
                List<string> before = new List<string>();
                before.Add(string.Format(Templates.namespaceKeyword + " {0}", structNamespace));
                before.Add(Templates.carriageOpenBrace);
                List<string> after = new List<string>();
                after.Add(Templates.carriageCloseBrace);
                writableLines = writableLines.SorroundWith(before, after);
            }

            Writer.CreateFile(fullPath, writableLines.ToArray());
        }
    }
}