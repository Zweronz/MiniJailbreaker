using UnityEngine;

public class GameAudioTrigger : MonoBehaviour
{
	public Transform[] AudioList;

	private void Start()
	{
		if (AudioList == null)
		{
			Debug.Log(string.Concat("transform = ", base.transform, " not found Sound "));
			return;
		}
		ITAudioEvent iTAudioEvent = null;
		for (int i = 0; i < AudioList.Length; i++)
		{
			iTAudioEvent = AudioList[i].GetComponent<ITAudioEvent>();
			if (null != iTAudioEvent)
			{
				iTAudioEvent.Trigger();
			}
		}
	}

	private void Update()
	{
	}
}
