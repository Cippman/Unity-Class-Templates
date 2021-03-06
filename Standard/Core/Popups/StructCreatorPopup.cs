﻿using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
#if UNITY_EDITOR
    public class StructCreatorPopup : TwoChoicesPopup
    {
        public string namespaceInputString = "";
        public string typeInputString = "MyStruct";

        public static StructCreatorPopup OpenPopup()
        {
            StructCreatorPopup window = CreateInstance<StructCreatorPopup>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 150);
            window.ShowPopup();
            return window;
        }
            
        protected override void Draw()
        {
            base.Draw();
            
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Insert a type and confirm!", EditorStyles.wordWrappedLabel);
            namespaceInputString = EditorGUILayout.TextField("Namespace", namespaceInputString);
            typeInputString = EditorGUILayout.TextField("Type", typeInputString);
            GUILayout.Space(50);
        }
    }
#endif
}