using UnityEngine;
using System;
using System.IO;
using CippSharp.ClassTemplates.Extensions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.ClassTemplates
{
    public static class Writer
    {
        /// <summary>
        /// Create a file at path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="lines"></param>
        public static void CreateFile(string fullPath, string[] lines)
        {
            if (string.IsNullOrEmpty(fullPath))
            {
                Debug.LogError("Given path is null or empty!");
                return;
            }
            
            string directory = Path.GetDirectoryName(fullPath);
            DirectoryUtility.CreateFolder(directory);
         
            if (File.Exists(fullPath))
            {
                Debug.LogError("File already exist!");
                return;
            }
            
            File.Create(fullPath).Dispose();
            FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate);
            TextWriter textWriter = new StreamWriter(fileStream);
            for (int i = 0; i < lines.Length; i++)
            {
                textWriter.WriteLine(lines[i]);
            }
            
            textWriter.Close();
            
#if UNITY_EDITOR
            if (directory.Contains("Assets/"))
            {
                AssetDatabase.ImportAsset(fullPath);
            }
#endif
        }

        /// <summary>
        /// Create a log file at directory + filename.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="fileName"></param>
        /// <param name="lines"></param>
        public static void CreateLogFile(string directory, string fileName, string[] lines)
        {
            UnityEngine.Debug.Log("Generating Log file.... Please wait!");
            Directory.CreateDirectory(directory);
            
            DateTime dateTime = DateTime.Now;
            string now = string.Format("[({0}d_{1}m_{2}y)_({3}h_{4}m_{5}s_{6}ms)]",
                dateTime.Day.ToString(), dateTime.Month.ToString(), dateTime.Year.ToString(),
                dateTime.Hour.ToString(), dateTime.Minute.ToString(), dateTime.Second.ToString(),
                dateTime.Millisecond.ToString());
            string completeFileName = string.Format("{0}_{1}.{2}", fileName, now, ".txt");
            directory = directory.Slash();
            string fullPath = string.Format("{0}{1}", directory, completeFileName);
            CreateFile(fullPath, lines);
            UnityEngine.Debug.Log("Generation Complete!");
        }

    }
}
