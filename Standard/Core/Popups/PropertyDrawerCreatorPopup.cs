//This class was offered by "Cippman".
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public class PropertyDrawerCreatorPopup : TwoChoicesPopup
    {
        public string namespaceInputString = "";
        public string typeInputString = "NewBehaviourScript";
#if UNITY_EDITOR
        public static PropertyDrawerCreatorPopup OpenPopup()
        {
            PropertyDrawerCreatorPopup window = CreateInstance<PropertyDrawerCreatorPopup>();
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
#endif
    }
}
