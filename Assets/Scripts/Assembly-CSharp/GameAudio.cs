using UnityEngine;

public class GameAudio : GamePool
{
	private const string c_strObjectPath = "Prefabs/Sound/";

	private const string c_strPoolName = "AudioEvent";

	private static GameAudio m_cmInstance;

	private GameAudio()
	{
	}

	public static GameAudio GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameAudio();
			m_cmInstance.Initialize("Prefabs/Sound/", "AudioEvent");
			GameObject gameObject = GameObject.Find("TAudioController");
			if (gameObject == null)
			{
				gameObject = new GameObject("TAudioController");
				gameObject.AddComponent(typeof(TAudioController));
				Object.DontDestroyOnLoad(gameObject);
			}
		}
		return m_cmInstance;
	}

	public new void Destroy()
	{
		base.Destroy();
		if (m_cmInstance != null)
		{
			m_cmInstance = null;
		}
	}

	public void playSound(string strCode, Vector3 v3Position)
	{
		if (strCode == null || strCode.Length == 0)
		{
			return;
		}
		Transform transform = citeObject(strCode);
		if (null == transform)
		{
			Debug.Log("cmSound is null " + strCode);
			return;
		}
		transform.gameObject.SetActiveRecursively(true);
		ITAudioEvent iTAudioEvent = (ITAudioEvent)transform.GetComponent(typeof(ITAudioEvent));
		if (!(null == iTAudioEvent))
		{
			transform.position = v3Position;
			iTAudioEvent.Trigger();
		}
	}

	public void resetPosition(string strCode, Vector3 v3Position)
	{
		if (strCode != null && strCode.Length != 0)
		{
			Transform transform = citeObject(strCode);
			if (!(null == transform))
			{
				transform.position = v3Position;
			}
		}
	}

	public void stopSound(string strCode)
	{
		if (strCode == null || strCode.Length == 0)
		{
			return;
		}
		Transform transform = citeObject(strCode);
		if (!(null == transform))
		{
			transform.gameObject.SetActiveRecursively(false);
			ITAudioEvent iTAudioEvent = (ITAudioEvent)transform.GetComponent(typeof(ITAudioEvent));
			if (!(null == iTAudioEvent))
			{
				iTAudioEvent.Stop();
			}
		}
	}

	public void playBackgroundSound(string strMusic)
	{
		GameObject gameObject = GameObject.Find(strMusic);
		if (!(null == gameObject))
		{
			ITAudioEvent iTAudioEvent = (ITAudioEvent)gameObject.GetComponent(typeof(ITAudioEvent));
			if (null != iTAudioEvent)
			{
				iTAudioEvent.Trigger();
			}
		}
	}
}
