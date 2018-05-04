using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
#if UNITY_EDITOR
    public class EmptyStructPopup : Popup
    {
        public event UnityAction<EmptyStructPopup> onClosePopup;

        public string typeInputString { get; private set;}
        public string namespaceInputString { get; private set; }
        public bool closeConfirmed { get; private set; }

        private Vector2 scrollPosition = Vector2.zero;

        public static EmptyStructPopup OpenPopup()
        {
            EmptyStructPopup window = CreateInstance<EmptyStructPopup>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 500, 150);
            window.ShowPopup();
            return window;
        }
        
        void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Please insert a valid type!", EditorStyles.wordWrappedLabel);
            bool gui = GUI.enabled;
            GUI.enabled = false;
            EditorGUILayout.TextField("Path", defaultDirectoryPath);
            GUI.enabled = gui;
            namespaceInputString = EditorGUILayout.TextField("Namespace", namespaceInputString);
            typeInputString = EditorGUILayout.TextField("Type", typeInputString);

            GUILayout.Space(50);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayoutUtility.DrawMiniButton("Confirm", OnConfirm);
            EditorGUILayoutUtility.DrawMiniButton("Close", OnClose);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
        }

        void OnConfirm()
        {
            closeConfirmed = true;
            ClosePopup();
        }

        void OnClose()
        {
            closeConfirmed = false;
            ClosePopup();
        }

        protected override void ClosePopup()
        {
            if (onClosePopup != null)
            {
                onClosePopup(this);
            }

            onClosePopup = null;

            this.Close();
        }

    }
#endif
}