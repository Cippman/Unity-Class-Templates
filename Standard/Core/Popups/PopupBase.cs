#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
#if UNITY_EDITOR
    public abstract class PopupBase : EditorWindow
    {
        //public static "abstract" Popup OpenPopup ()

        private void OnEnable()
        {
            GetReferences();
            RegisterEvents();
        }

        protected virtual void GetReferences()
        {

        }


        protected virtual void RegisterEvents()
        {

        }

        private void OnDestroy()
        {
            UnregisterEvents();
            ClearReferences();
        }

        private void OnDisable()
        {
            UnregisterEvents();
            ClearReferences();
        }

        protected virtual void ClearReferences()
        {

        }

        protected virtual void UnregisterEvents()
        {

        }

        protected virtual void ClosePopup()
        {

        }
    }
#endif
}
