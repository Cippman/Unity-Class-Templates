using UnityEditor;
using UnityEngine;

namespace CippSharp.ClassTemplates
{
    public class TwoChoicesPopup : Popup
    {
        public bool confirmed { get; private set; }

        private Vector2 scrollPosition = Vector2.zero;

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            EditorGUILayoutUtility.DrawDisabledGUI(DisabledHeaderGUI);
            Draw();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayoutUtility.DrawMiniButton("Confirm", OnConfirm);
            EditorGUILayoutUtility.DrawMiniButton("Close", OnClose);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndScrollView();
            
        }

        protected virtual void DisabledHeaderGUI()
        {
            EditorGUILayout.ObjectField("Self", this, typeof(TwoChoicesPopup), false);
            EditorGUILayout.TextField("Selected Path", selectedPath);
        }

        protected virtual void Draw()
        {
            
        }
        
        
        protected virtual void OnConfirm()
        {
            confirmed = true;
            ClosePopup();
        }

        protected virtual void OnClose()
        {
            confirmed = false;
            ClosePopup();
        }
        
        protected override void ClosePopup()
        {
            OnClosePopup(this);
            
            this.Close();
        }
    }
}