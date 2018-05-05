using UnityEngine;
using UnityEngine.Events;

namespace CippSharp.ClassTemplates
{
	#if UNITY_EDITOR
	public class Popup : PopupBase
	{
		public event UnityAction<Popup> onClosePopup;
		
		public string selectedPath { get; private set;}

		protected override void GetReferences()
		{
			base.GetReferences();
			selectedPath = AssetDatabaseUtility.GetSelectedPathOrFallback();
		}

		protected virtual void OnClosePopup(Popup popup)
		{
			var handler = onClosePopup;
			if (handler != null) handler(popup);
		}
	}
	#endif
}