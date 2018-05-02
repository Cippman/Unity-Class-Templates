﻿using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public static class EditorGUILayoutUtility
    {
        #if UNITY_EDITOR
        public static void DrawMiniButton(string buttonName, UnityAction action)
        {
            if (GUILayout.Button(buttonName, EditorStyles.miniButton))
            {
                action();
            }
        }
        #endif
    }
}
