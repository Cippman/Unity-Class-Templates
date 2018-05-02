using UnityEngine;

namespace CippSharp.ClassTemplates
{
	#if UNITY_EDITOR
	public class Popup : PopupBase
	{
		public string defaultDirectoryPath { get; private set;}

		protected override void GetReferences()
		{
			base.GetReferences();
			defaultDirectoryPath = AssetDatabaseUtility.GetSelectedPathOrFallback();
		}
	}
	#endif
}