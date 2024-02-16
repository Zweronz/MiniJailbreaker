using System;
using System.IO;
using UnityEngine;

public class Utils
{
	private static string m_SavePath;

	static Utils()
	{
		string text = Application.dataPath;
		if (!Application.isEditor)
		{
			text = text.Substring(0, text.LastIndexOf('/'));
			text = text.Substring(0, text.LastIndexOf('/'));
		}
		text += "/Documents";
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		m_SavePath = text;
	}

	public static bool CreateDocumentSubDir(string dirname)
	{
		string path = m_SavePath + "/" + dirname;
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
			return true;
		}
		return false;
	}

	public static void DeleteDocumentDir(string dirname)
	{
		string path = m_SavePath + "/" + dirname;
		if (Directory.Exists(path))
		{
			Directory.Delete(path, true);
		}
	}

	public static string SavePath()
	{
		return m_SavePath;
	}

	public static string GetTextAsset(string txt_name)
	{
		TextAsset textAsset = Resources.Load(txt_name) as TextAsset;
		if (null != textAsset)
		{
			return textAsset.text;
		}
		return string.Empty;
	}

	public static void FileSaveString(string name, string content)
	{
		string text = SavePath() + "/" + name;
		try
		{
			FileStream fileStream = new FileStream(text, FileMode.Create);
			StreamWriter streamWriter = new StreamWriter(fileStream);
			streamWriter.Write(content);
			streamWriter.Close();
			fileStream.Close();
		}
		catch
		{
			Debug.Log("Save" + text + " error");
		}
	}

	public static void FileGetString(string name, ref string content)
	{
		string text = SavePath() + "/" + name;
		if (!File.Exists(text))
		{
			return;
		}
		try
		{
			FileStream fileStream = new FileStream(text, FileMode.Open);
			StreamReader streamReader = new StreamReader(fileStream);
			content = streamReader.ReadToEnd();
			streamReader.Close();
			fileStream.Close();
		}
		catch
		{
			Debug.Log("Load" + text + " error");
		}
	}

	public static bool IsRetina()
	{
		//if (UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPhone4 || UnityEngine.iOS.Device.generation == UnityEngine.iOS.DeviceGeneration.iPodTouch4Gen)
		//{
		//	return true;
		//}
		return true;
	}

	public static bool ProbabilityIsRandomHit(float rate)
	{
		float num = UnityEngine.Random.Range(0f, 1f);
		float num2 = rate / 2f;
		float num3 = UnityEngine.Random.Range(num2, 1f - num2);
		if (num3 - num2 < num && num < num3 + num2)
		{
			return true;
		}
		return false;
	}

	public static bool IsChineseLetter(string input)
	{
		for (int i = 0; i < input.Length; i++)
		{
			int num = Convert.ToInt32(Convert.ToChar(input.Substring(i, 1)));
			if (num >= 128)
			{
				return true;
			}
		}
		return false;
	}
}
