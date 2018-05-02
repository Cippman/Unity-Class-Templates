using System.IO;
using System.Diagnostics;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Cipp.ClassCreation
{
	public class TemplateWriter
	{

		#region singleton

		static TemplateWriter instance;

		public static TemplateWriter Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new TemplateWriter();
				}

				return instance;
			}
		}

		#endregion

		public static void CreateFolder(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}
		}

		public static void CreateClassFile(string path, string[] lines)
		{
			if (File.Exists(path))
			{
				UnityEngine.Debug.Log("File already exist, please specify another name!");
			}
			else
			{
				File.Create(path).Dispose();
				FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
				TextWriter tw = new StreamWriter(fs);
				int i = 0;
				while (i < lines.Length)
				{
					tw.WriteLine(lines[i]);
					i++;
				}

				tw.Close();
#if UNITY_EDITOR
				AssetDatabase.ImportAsset(path);
#endif
			}
		}

		public static void CreateLogFile(string name, string[] lines)
		{
			UnityEngine.Debug.Log("Generating Log file.... Please wait!");
			var date = System.DateTime.Now;
			TemplateWriter.CreateFolder("C:/UnityProjects/Logs/");
			string now = date.Day + "_" + date.Month + "_" + date.Year + "_" + date.Hour + "_" + date.Minute + "_" +
			             date.Second + "_" + date.Millisecond;
			string path = "C:/UnityProjects/Logs/" + name + "_" + now + ".txt";
			if (File.Exists(path))
			{
				UnityEngine.Debug.Log("File already exist, please specify another name!");
			}
			else
			{
				File.Create(path).Dispose();
				FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
				TextWriter tw = new StreamWriter(fs);
				int i = 0;
				while (i < lines.Length)
				{
					tw.WriteLine(lines[i]);
					i++;
				}

				tw.Close();

				Process.Start(@"c:/UnityProjects/Logs/");

				UnityEngine.Debug.Log("Generation Complete!");
			}
		}
	}
}