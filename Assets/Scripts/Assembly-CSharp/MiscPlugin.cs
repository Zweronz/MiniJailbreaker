using System.Runtime.InteropServices;

public class MiscPlugin
{
	[DllImport("__Internal")]
	protected static extern void TakePhotoPlugin(string save_path, string photo_key);

	public static void TakePhoto(string photo_key)
	{
		TakePhotoPlugin(Utils.SavePath(), photo_key);
	}

	[DllImport("__Internal")]
	protected static extern bool OSIsIAPCrack();

	[DllImport("__Internal")]
	protected static extern bool OSIsJailbreak();

	[DllImport("__Internal")]
	protected static extern void SendMail(string address, string subject, string content);

	[DllImport("__Internal")]
	protected static extern int MessgeBox1(string title, string message, string button);

	[DllImport("__Internal")]
	protected static extern int MessgeBox2(string title, string message, string button1, string button2);

	public static bool CheckOSIsIAPCrack()
	{
		return OSIsIAPCrack();
	}

	public static bool CheckOSIsJailbreak()
	{
		return OSIsJailbreak();
	}

	public static void ToSendMail(string address, string subject, string content)
	{
		SendMail(address, subject, content);
	}

	public static int ShowMessageBox1(string title, string message, string button)
	{
		return MessgeBox1(title, message, button);
	}

	public static int ShowMessageBox2(string title, string message, string button1, string button2)
	{
		return MessgeBox2(title, message, button1, button2);
	}

	[DllImport("__Internal")]
	protected static extern void ShowIndicator(int style, bool iPad, float r, float g, float b, float a);

	public static void ShowIndicatorSystem(int style, bool iPad, float r, float g, float b, float a)
	{
		ShowIndicator(style, iPad, r, g, b, a);
	}

	[DllImport("__Internal")]
	protected static extern void HideIndicator();

	public static void HideIndicatorSystem()
	{
		HideIndicator();
	}
}
