using System;
using System.IO;
using UnityEngine;

namespace CippSharp.ClassTemplates
{
    public static class DirectoryUtility 
    {
        /// <summary>
        /// Create folder at given path.
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void CreateFolder(string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentNullException();
            }

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
