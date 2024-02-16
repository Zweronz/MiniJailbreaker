using System.Collections;
using UnityEngine;

public class GameLoading : MonoBehaviour
{
	public ITAudioEvent musicRef;

	private int m_nLoadCount;

	private IEnumerator Start()
	{
		OpenClikPlugin.Show(false);
		yield return 0;
		Transform cmCamera = GameObject.Find("CameraBox/Main Camera").transform;
		UIEventManager UI = cmCamera.GetComponent(typeof(UIEventManager)) as UIEventManager;
		UI.doSwitch(true, null);
		yield return 0;
		m_nLoadCount = 0;
		ITAudioEvent music = Object.Instantiate(musicRef) as ITAudioEvent;
		music.Trigger();
		globalVal.GameStatus = GAME_STATUS.GAME_NONE;
		yield return Application.LoadLevelAdditiveAsync("scene_game");
		yield return 0;
		globalVal.GameStatus = GAME_STATUS.GAME_LOAD;
		globalVal.ReadFile("saveData.txt");
	}

	private void Update()
	{
		if (globalVal.GameStatus == GAME_STATUS.GAME_START)
		{
			base.transform.GetComponent<GameLoading>().enabled = false;
			Transform transform = GameObject.Find("CameraBox/Main Camera").transform;
			transform.GetComponent<Camera>().nearClipPlane = 0.3f;
			transform.GetComponent<Camera>().farClipPlane = 1000f;
			return;
		}
		Transform transform2 = globalVal.UIScript.transform.Find("gameloading/loading");
		TUIMeshSprite component = transform2.GetComponent<TUIMeshSprite>();
		switch (m_nLoadCount / 3 % 3)
		{
		case 0:
			component.frameName = "loading_01";
			break;
		case 1:
			component.frameName = "loading_02";
			break;
		case 2:
			component.frameName = "loading_03";
			break;
		}
		component.UpdateMesh();
		m_nLoadCount++;
	}
}
