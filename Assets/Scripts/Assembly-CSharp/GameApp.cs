using UnityEngine;

public class GameApp
{
	private const int c_nResurrectionCount = 99999;

	private GameView m_cmView;

	private GamePlayer m_cmPlayer;

	private GameFixList m_cmFixList;

	private string m_strNextScene;

	private int m_nLoadCount;

	private float fReadyTime;

	private float m_fLightningTime;

	private float m_fResurrectionTime;

	private string[] m_alTerrain;

	private string[] m_alSubassembly;

	private string[] m_alItem;

	private string[] m_alMonster;

	private int nTerrainIndex;

	private int nSubassemblyIndex;

	private int nItemIndex;

	private int nMonsterIndex;

	private int m_nResurrection;

	public string NextScene
	{
		set
		{
			m_strNextScene = value;
		}
	}

	public GameApp()
	{
		m_cmView = new GameView();
		m_cmPlayer = new GamePlayer();
		m_cmFixList = GameFixList.GetInstance();
	}

	public void Initialize()
	{
		globalVal.Config.Initialize();
		globalVal.RandomType = ENUM_DIFFICULTY_TYPE.DIFFICULTY_LEVEL1;
		m_cmPlayer.Initialize();
		m_cmView.Initialize(m_cmPlayer.Player);
		globalVal.Terrain.Player = m_cmPlayer.Player;
		m_nResurrection = 99999;
		globalVal.AttackPlayer = false;
		globalVal.UIState = UILayer.GAME;
		globalVal.EndTime = 0f;
		globalVal.InitRecord();
		globalVal.Player = m_cmPlayer.Player;
		globalVal.PlayerBip = m_cmPlayer.PlayerBip;
		TAudioManager.instance.AudioListener.transform.position = m_cmPlayer.position;
		TAudioManager.instance.AudioListener.transform.forward = m_cmPlayer.Player.forward;
		m_fLightningTime = Time.realtimeSinceStartup;
		GameAudio.GetInstance().playSound("Amb_sences", Vector3.zero);
		OpenClikPlugin.Hide();
	}

	public void Destroy()
	{
		m_cmPlayer.Destroy();
		m_cmView.Destroy();
		m_cmFixList.Destroy();
		globalVal.Destroy();
		Transform transform = GameObject.Find("CameraBox/Main Camera").transform;
		UIEventManager uIEventManager = transform.GetComponent(typeof(UIEventManager)) as UIEventManager;
		uIEventManager.doSwitch(false, m_strNextScene);
	}

	public void Retry()
	{
		m_cmFixList.Reset();
		globalVal.Reset();
		m_cmPlayer.Reset();
		m_cmView.Reset();
		showItemDialog(false);
	}

	public void Run()
	{
		if (globalVal.GamePause)
		{
			return;
		}
		switch (globalVal.GameStatus)
		{
		case GAME_STATUS.GAME_LOAD:
			globalVal.GameStatus = GAME_STATUS.GAME_INITIALIZE;
			break;
		case GAME_STATUS.GAME_INITIALIZE:
			Initialize();
			globalVal.GameStatus = GAME_STATUS.GAME_START;
			globalVal.UIScript.ShowLoading(false);
			globalVal.UIScript.refurbishDistance();
			globalVal.GameScore = globalVal.AllGold;
			globalVal.UIScript.refurbishScore();
			break;
		case GAME_STATUS.GAME_START:
			if (0f <= m_cmPlayer.position.z)
			{
				globalVal.GameStatus = GAME_STATUS.GAME_RUNNING;
				globalVal.ReadyTime = Time.realtimeSinceStartup;
				globalVal.StartTime = Time.realtimeSinceStartup;
				globalVal.CurDistance = 0f;
			}
			AssistRun();
			m_cmPlayer.addShadow();
			break;
		case GAME_STATUS.GAME_READY:
		{
			Time.timeScale = 0.0001f;
			float num = Time.realtimeSinceStartup - globalVal.ReadyTime;
			if (0f <= num && 0.1f >= num)
			{
				globalVal.UIScript.showPicture("time3", true);
			}
			if (1f <= num && 1.1f >= num)
			{
				globalVal.UIScript.showPicture("time2", true);
			}
			if (2f <= num && 2.1f >= num)
			{
				globalVal.UIScript.showPicture("time1", true);
			}
			if (3f <= num)
			{
				globalVal.UIScript.showPicture(string.Empty, false);
				globalVal.GameStatus = GAME_STATUS.GAME_RUNNING;
				Time.timeScale = 1f;
			}
			m_cmPlayer.doAction();
			AssistRun();
			break;
		}
		case GAME_STATUS.GAME_RUNNING:
		case GAME_STATUS.GAME_MOIVE:
		case GAME_STATUS.GAME_WORMHOLE:
		case GAME_STATUS.GAME_RESURRECTION:
			globalVal.UIScript.refurbishDistance();
			m_cmPlayer.doAction();
			AssistRun();
			break;
		case GAME_STATUS.GAME_OVER:
			Time.timeScale = 1f;
			if (globalVal.EndTime == 0f)
			{
				globalVal.EndTime = Time.realtimeSinceStartup;
			}
			else if (!(3f > Time.realtimeSinceStartup - globalVal.EndTime))
			{
				if (!checkResurrection())
				{
					globalVal.GameStatus = GAME_STATUS.GAME_ACCOUNT;
				}
				globalVal.NextGameStatus = GAME_STATUS.GAME_ACCOUNT;
				globalVal.Effect.Run();
			}
			break;
		case GAME_STATUS.GAME_DESTROY:
			globalVal.UIState = UILayer.GAMEOVER;
			Destroy();
			break;
		case GAME_STATUS.GAME_ACCOUNT:
			globalVal.GameStatus = GAME_STATUS.GAME_DESTROY;
			NextScene = "scene_ui_gameover";
			if (0 <= globalVal.CurDeath && globalVal.Death.Length > globalVal.CurDeath)
			{
				globalVal.Death[globalVal.CurDeath]++;
			}
			break;
		case GAME_STATUS.GAME_DIALOG:
			break;
		case GAME_STATUS.GAME_PAUSE:
			break;
		case GAME_STATUS.GAME_KNOCK:
			break;
		case GAME_STATUS.GAME_TEACH:
			break;
		case GAME_STATUS.GAME_PLAYBACK:
			globalVal.SaveFile("saveData.txt");
			if (globalVal.Limt)
			{
				globalVal.GameStatus = GAME_STATUS.GAME_RESURRECTION;
			}
			else
			{
				globalVal.GameStatus = GAME_STATUS.GAME_OVER;
			}
			break;
		case GAME_STATUS.GAME_STRIKE1:
		case GAME_STATUS.GAME_STRIKE2:
		case GAME_STATUS.GAME_STRIKE3:
		case GAME_STATUS.GAME_STRIKE4:
		case GAME_STATUS.GAME_DROP:
			break;
		}
	}

	private void AssistRun()
	{
		globalVal.Effect.Run();
		globalVal.Terrain.Run();
		m_cmFixList.Run();
		if (!(null == TAudioManager.instance.AudioListener))
		{
			TAudioManager.instance.AudioListener.transform.position = m_cmPlayer.position;
			TAudioManager.instance.AudioListener.transform.forward = m_cmPlayer.Player.forward;
		}
	}

	public void showLightning()
	{
		Transform transform = globalVal.UIScript.transform.Find("Lightning");
		if (null != transform)
		{
			LightningScreen lightningScreen = transform.GetComponent(typeof(LightningScreen)) as LightningScreen;
			if (null != lightningScreen)
			{
				lightningScreen.StartGlitter();
			}
		}
	}

	public void View()
	{
		if (globalVal.GamePause)
		{
			return;
		}
		switch (globalVal.GameStatus)
		{
		case GAME_STATUS.GAME_START:
			m_cmView.StartScreen();
			break;
		case GAME_STATUS.GAME_RUNNING:
		case GAME_STATUS.GAME_MOIVE:
		case GAME_STATUS.GAME_WORMHOLE:
		case GAME_STATUS.GAME_KNOCK:
		case GAME_STATUS.GAME_RESURRECTION:
		case GAME_STATUS.GAME_TEACH:
		case GAME_STATUS.GAME_STRIKE1:
		case GAME_STATUS.GAME_STRIKE2:
		case GAME_STATUS.GAME_STRIKE3:
		case GAME_STATUS.GAME_STRIKE4:
		case GAME_STATUS.GAME_DROP:
		case GAME_STATUS.GAME_PLAYBACK:
			m_cmView.RunningScreen();
			if (globalVal.VibrateScreen)
			{
				m_cmView.startVibrate(1.5f);
				globalVal.VibrateScreen = false;
			}
			break;
		case GAME_STATUS.GAME_OVER:
		case GAME_STATUS.GAME_ACCOUNT:
		case GAME_STATUS.GAME_DESTROY:
		case GAME_STATUS.GAME_DIALOG:
			break;
		}
	}

	public void Logic()
	{
		if (globalVal.GamePause)
		{
			return;
		}
		switch (globalVal.GameStatus)
		{
		case GAME_STATUS.GAME_READY:
		case GAME_STATUS.GAME_START:
		case GAME_STATUS.GAME_RUNNING:
			m_cmPlayer.recordInput();
			m_cmPlayer.PhysicsMove();
			break;
		case GAME_STATUS.GAME_MOIVE:
			m_cmPlayer.PlayMoive();
			break;
		case GAME_STATUS.GAME_WORMHOLE:
			m_cmPlayer.wormhole();
			break;
		case GAME_STATUS.GAME_RESURRECTION:
			if (m_fResurrectionTime == 0f)
			{
				m_cmPlayer.resurrection();
				m_fResurrectionTime = Time.realtimeSinceStartup + 1f;
			}
			else if (m_fResurrectionTime != 0f && Time.realtimeSinceStartup >= m_fResurrectionTime)
			{
				globalVal.GameStatus = GAME_STATUS.GAME_READY;
				m_fResurrectionTime = 0f;
				globalVal.EndTime = 0f;
			}
			break;
		case GAME_STATUS.GAME_KNOCK:
			m_cmPlayer.knock();
			break;
		case GAME_STATUS.GAME_TEACH:
			break;
		case GAME_STATUS.GAME_STRIKE1:
			m_cmPlayer.strike1();
			break;
		case GAME_STATUS.GAME_STRIKE2:
			m_cmPlayer.strike2();
			break;
		case GAME_STATUS.GAME_STRIKE3:
			m_cmPlayer.strike3();
			break;
		case GAME_STATUS.GAME_STRIKE4:
			m_cmPlayer.strike4();
			break;
		case GAME_STATUS.GAME_DROP:
			m_cmPlayer.Drop();
			break;
		case GAME_STATUS.GAME_OVER:
		case GAME_STATUS.GAME_ACCOUNT:
		case GAME_STATUS.GAME_DESTROY:
		case GAME_STATUS.GAME_DIALOG:
			break;
		}
	}

	public void LoadTerrain()
	{
		Debug.Log("LoadTerrain = " + m_alTerrain[nTerrainIndex]);
		nTerrainIndex++;
	}

	public void LoadSubassembly()
	{
		Debug.Log("LoadSubassembly = " + m_alSubassembly[nSubassemblyIndex]);
		nSubassemblyIndex++;
	}

	public void LoadItem()
	{
		Debug.Log("LoadItem = " + m_alItem[nItemIndex]);
		nItemIndex++;
	}

	public void LoadMonster()
	{
		Debug.Log("LoadMonster = " + m_alMonster[nMonsterIndex]);
		nMonsterIndex++;
	}

	public bool LoadPrefab()
	{
		if (m_alTerrain == null)
		{
			m_alTerrain = globalVal.Config.getAllTerrainKey();
			nTerrainIndex = 0;
		}
		if (nTerrainIndex < m_alTerrain.Length)
		{
			LoadTerrain();
			return false;
		}
		if (m_alSubassembly == null)
		{
			m_alSubassembly = globalVal.Config.getAllSubassemblyKey();
			nSubassemblyIndex++;
		}
		if (nSubassemblyIndex < m_alSubassembly.Length)
		{
			LoadSubassembly();
			return false;
		}
		if (m_alItem == null)
		{
			m_alItem = globalVal.Config.getAllItemKey();
			nItemIndex = 0;
		}
		if (nItemIndex < m_alItem.Length)
		{
			LoadItem();
			return false;
		}
		if (m_alMonster == null)
		{
			m_alMonster = globalVal.Config.getAllMonsterKey();
			nMonsterIndex = 0;
		}
		if (nMonsterIndex < m_alMonster.Length)
		{
			LoadMonster();
			return false;
		}
		return true;
	}

	private bool checkKMItem()
	{
		return false;
	}

	private bool checkResurrection()
	{
		return false;
	}

	public void showItemDialog(bool bShow)
	{
		globalVal.UIScript.ShowItemDialog(bShow);
		globalVal.GameStatus = GAME_STATUS.GAME_DIALOG;
	}
}
