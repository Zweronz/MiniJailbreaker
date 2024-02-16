using UnityEngine;

public class Game : MonoBehaviour
{
	private GameApp m_cmGame;

	private float m_fTime;

	private int m_nFPS;

	private UIEventManager m_cmUI;

	private void Awake()
	{
		Transform transform = GameObject.Find("CameraBox/Main Camera").transform;
		m_cmUI = transform.GetComponent(typeof(UIEventManager)) as UIEventManager;
	}

	private void Start()
	{
		m_cmGame = new GameApp();
	}

	private void Update()
	{
		float num = 0f;
		float num2 = 0f;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		m_cmGame.Run();
		num2 = Time.realtimeSinceStartup - realtimeSinceStartup;
		m_nFPS++;
		if (m_fTime + 1f <= Time.realtimeSinceStartup)
		{
			globalVal.FPS = m_nFPS;
			m_fTime = Time.realtimeSinceStartup;
			m_nFPS = 0;
		}
	}

	private void LateUpdate()
	{
		m_cmGame.View();
	}

	private void FixedUpdate()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		m_cmGame.Logic();
		float num = Time.realtimeSinceStartup - realtimeSinceStartup;
	}

	public void Destroy()
	{
		globalVal.GameStatus = GAME_STATUS.GAME_DESTROY;
		m_cmGame.NextScene = "scene_ui_menu";
	}

	public void Retry()
	{
		m_cmGame.Retry();
		globalVal.GameStatus = GAME_STATUS.GAME_LOAD;
		globalVal.UIState = UILayer.GAME;
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			globalVal.GamePause = true;
			Time.timeScale = 0f;
			return;
		}
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
		menu2.ShowOption(true);
		OpenClikPlugin.Show(false);
	}
}
