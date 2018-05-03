using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
	public static class AssetDatabaseUtility
	{
#if UNITY_EDITOR
		/// <summary>
		/// Return selected path or fallback to assets directory.
		/// </summary>
		/// <returns></returns>
		public static string GetSelectedPathOrFallback()
		{
			string path = "Assets";

			foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
			{
				path = AssetDatabase.GetAssetPath(obj);
				if (!string.IsNullOrEmpty(path) && File.Exists(path))
				{
					path = Path.GetDirectoryName(path);
					break;
				}
			}

			return path;
		}
#endif

		public static string GetAssetPath(string filename, string filter, string[] folders = null)
		{
			string[] assetsPaths = GetAssetsPath(filter, folders);
			
			if (assetsPaths == null || assetsPaths.Length < 1)
			{
				return string.Empty;
			}

			foreach (string assetPath in assetsPaths)
			{
				if (Path.GetFileNameWithoutExtension(assetPath) == filename)
				{
					return assetPath;
				}
			}

			return string.Empty;
		}

#if UNITY_EDITOR
		/// <summary>
		/// Return paths of filtered file-assets.
		/// </summary>
		/// <param name="filter"></param>
		/// <param name="folders"></param>
		/// <returns></returns>
		public static string[] GetAssetsPath(string filter, string[] folders = null)
		{
			string[] guids = AssetDatabase.FindAssets(filter, folders);
			string[] paths = new string[guids.Length];
			for (int i = 0; i < guids.Length; i++)
			{
				paths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
			}

			return paths;
		}
#endif
	}
}