using System.Runtime.InteropServices;

public class OpenClikPlugin
{
	private enum Status
	{
		kShowFull = 0,
		kShowTip = 1,
		kHide = 2
	}

	private static Status s_Status;

	[DllImport("__Internal")]
	protected static extern void OpenClik_Initialize(string key);

	[DllImport("__Internal")]
	protected static extern void OpenClik_Show(bool show_full);

	[DllImport("__Internal")]
	protected static extern void OpenClik_Hide();

	public static void Initialize(string key)
	{
//		OpenClik_Initialize(key);
		s_Status = Status.kHide;
	}

	public static void Show(bool show_full)
	{
		if (s_Status == Status.kHide)
		{
			//OpenClik_Show(show_full);
			if (show_full)
			{
				s_Status = Status.kShowFull;
			}
			else
			{
				s_Status = Status.kShowTip;
			}
		}
		else if (s_Status == Status.kShowFull)
		{
			if (!show_full)
			{
				//OpenClik_Show(show_full);
				s_Status = Status.kShowTip;
			}
		}
		else if (s_Status == Status.kShowTip && show_full)
		{
			//OpenClik_Show(show_full);
			s_Status = Status.kShowFull;
		}
	}

	public static void Hide()
	{
		s_Status = Status.kHide;
//		OpenClik_Hide();
	}
}
