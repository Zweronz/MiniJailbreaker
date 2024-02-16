using System.Collections;
using System.IO;
using UnityEngine;

public class globalVal
{
	public const float RunwayWidth = 1.5f;

	private const string c_strFileName = "3D_Run";

	public static readonly string version = "1.0";

	public static int[] cur_task_id = new int[3];

	public static Transform sceneLastTrans = null;

	public static bool secondKey = false;

	public static float changeDis = 1000f;

	public static bool isskipkey = false;

	public static float MoveDistance = 0f;

	public static bool MoveEnd = true;

	public static UILayer UIState = UILayer.MENU;

	public static bool FirstCome = false;

	public static int GameCount = 0;

	public static bool music = true;

	public static bool sound = true;

	public static bool load = false;

	public static int MonsterCount = 0;

	public static Transform Player = null;

	public static Transform PlayerBip = null;

	private static GAME_STATUS m_cmGameStatus = GAME_STATUS.GAME_LOAD;

	public static GAME_STATUS NextGameStatus = GAME_STATUS.GAME_READY;

	public static float ReadyTime = 0f;

	public static ENUM_DIFFICULTY_TYPE RandomType = ENUM_DIFFICULTY_TYPE.DIFFICULTY_LEVEL1;

	public static ENUM_AREA_TYPE AreaType = ENUM_AREA_TYPE.MAX_AREA;

	public static int IAPIndex = -1;

	public static Camera MainCamera = null;

	public static bool GameJumpClick = false;

	public static bool GameGunClick = false;

	public static bool GamePause = false;

	public static bool GameReady = true;

	public static bool AttackPlayer = false;

	public static bool VibrateScreen = false;

	public static bool SearchLight = false;

	public static bool JumpStart = false;

	public static float GoldRotateAngle = 0f;

	public static Vector3 GoldPoint = Vector3.zero;

	public static float SpeedPower = 1f;

	public static float ItemCheckRange = 0f;

	public static int GoldPower = 1;

	public static int ShieldCount = 0;

	public static bool Invincibility = false;

	public static bool ShowLightWay = false;

	public static bool ShowFarise = true;

	public static bool Wormhole = false;

	public static bool Exchange = false;

	public static string CurItemName;

	public static bool OnGround = false;

	public static bool Limt = true;

	public static float StartTime = 0f;

	public static float EndTime = 0f;

	public static float FPS = 0f;

	private static menu m_cmMenu;

	public static float[] Distance = new float[8];

	public static int[] Gold = new int[8];

	public static int[] Monster = new int[8];

	public static int[] UseItemList = new int[24];

	public static int[] TreasureBox = new int[24];

	public static int CurDeath;

	public static int[] Death = new int[Config.getDeathCount()];

	private static string[] c_strAccomplishment = new string[9] { "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii" };

	public static bool[] Accomplishment = new bool[c_strAccomplishment.Length * 2];

	public static int AccomplishmentScore = 0;

	public static int AccomplishmentGold = 0;

	public static int GameScore = 0;

	private static CharaInfo? m_cmCharaInfo;

	private static GameInfo m_cmGameInfo = null;

	private static GameChannelManage m_cmGameChannel = null;

	private static GameSubassemblyManage m_cmSubassembly = null;

	private static GameEffect m_cmEffect = null;

	private static GameAudio m_cmAudio = null;

	private static GameTerrainManage m_cmTerrain = null;

	public static Vector3 GunTarget = Vector3.zero;

	public static int g_gold = 0;

	public static int g_avatar_id = 0;

	public static int g_bestScore = 0;

	public static int g_bestDistance = 0;

	public static bool g_bSumbit = true;

	public static ArrayList g_itemlevel = new ArrayList();

	public static ArrayList g_item_once_count = new ArrayList();

	public static ArrayList g_avatar_isbuy = new ArrayList();

	public static GAME_STATUS GameStatus
	{
		get
		{
			return m_cmGameStatus;
		}
		set
		{
			m_cmGameStatus = value;
		}
	}

	public static menu UIScript
	{
		get
		{
			if (null == m_cmMenu)
			{
				GameObject gameObject = GameObject.Find("TUI/TUIControl");
				if (null != gameObject)
				{
					m_cmMenu = gameObject.GetComponent(typeof(menu)) as menu;
				}
			}
			return m_cmMenu;
		}
	}

	public static float CurDistance
	{
		get
		{
			return Distance[6];
		}
		set
		{
			Distance[6] = value;
		}
	}

	public static float AllDistance
	{
		get
		{
			return Distance[7];
		}
		set
		{
			Distance[7] = value;
		}
	}

	public static int CurGold
	{
		get
		{
			return Gold[6];
		}
		set
		{
			Gold[6] = value;
		}
	}

	public static int AllGold
	{
		get
		{
			return Gold[7];
		}
		set
		{
			Gold[7] = value;
		}
	}

	public static int CurMonster
	{
		get
		{
			return Monster[6];
		}
		set
		{
			Monster[6] = value;
			GameObject gameObject = GameObject.Find("TUI/TUIControl");
			menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
			menu2.updateMonsterCount();
		}
	}

	public static int AllMonster
	{
		get
		{
			return Monster[7];
		}
		set
		{
			Monster[7] = value;
		}
	}

	public static int AllDeathCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < Death.Length; i++)
			{
				num += Death[i];
			}
			return num;
		}
	}

	public static CharaInfo CharaInfo
	{
		get
		{
			CharaInfo? cmCharaInfo = m_cmCharaInfo;
			if (!cmCharaInfo.HasValue)
			{
				m_cmCharaInfo = Config.getCharaInfoByID(g_avatar_id + 1);
			}
			CharaInfo? cmCharaInfo2 = m_cmCharaInfo;
			return cmCharaInfo2.Value;
		}
	}

	public static GameInfo Config
	{
		get
		{
			if (m_cmGameInfo == null)
			{
				m_cmGameInfo = GameInfo.GetInstance();
			}
			return m_cmGameInfo;
		}
	}

	public static GameChannelManage Channel
	{
		get
		{
			if (m_cmGameChannel == null)
			{
				m_cmGameChannel = GameChannelManage.GetInstance();
			}
			return m_cmGameChannel;
		}
	}

	public static GameSubassemblyManage Subassembly
	{
		get
		{
			if (m_cmSubassembly == null)
			{
				m_cmSubassembly = GameSubassemblyManage.GetInstance();
			}
			return m_cmSubassembly;
		}
	}

	public static GameEffect Effect
	{
		get
		{
			if (m_cmEffect == null)
			{
				m_cmEffect = GameEffect.GetInstance();
			}
			return m_cmEffect;
		}
	}

	public static GameAudio Audio
	{
		get
		{
			if (m_cmAudio == null)
			{
				m_cmAudio = GameAudio.GetInstance();
			}
			return m_cmAudio;
		}
	}

	public static GameTerrainManage Terrain
	{
		get
		{
			if (m_cmTerrain == null)
			{
				m_cmTerrain = GameTerrainManage.GetInstance();
				m_cmTerrain.Initialize();
			}
			return m_cmTerrain;
		}
	}

	public static void Destroy()
	{
		SaveFile("3D_Run");
		if (m_cmGameChannel != null)
		{
			m_cmGameChannel.Destroy();
		}
		m_cmGameChannel = null;
		if (m_cmSubassembly != null)
		{
			m_cmSubassembly.Destroy();
		}
		m_cmSubassembly = null;
		if (m_cmEffect != null)
		{
			m_cmEffect.Destroy();
		}
		m_cmEffect = null;
		if (m_cmAudio != null)
		{
			m_cmAudio.Destroy();
		}
		m_cmAudio = null;
		if (m_cmTerrain != null)
		{
			m_cmTerrain.Destroy();
		}
		m_cmTerrain = null;
	}

	public static void Reset()
	{
		if (m_cmTerrain != null)
		{
			m_cmTerrain.Reset();
		}
	}

	public static void InitRecord()
	{
		for (int i = 0; i < 7; i++)
		{
			Distance[i] = 0f;
			Gold[i] = 0;
			Monster[i] = 0;
		}
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
		menu2.updateMonsterCount();
	}

	public static string getSavePath()
	{
		string text = Application.dataPath;
		if (!Application.isEditor)
		{
			text = text.Substring(0, text.LastIndexOf('/'));
			text = text.Substring(0, text.LastIndexOf('/'));
		}
		return text + "/Documents/";
	}

	public static void SaveFile(string filename)
	{
		try
		{
			filename = getSavePath() + filename;
			Debug.Log(filename);
			FileStream fileStream = new FileStream(filename, FileMode.Create);
			BinaryWriter binaryWriter = new BinaryWriter(fileStream);
			binaryWriter.Write(AllGold);
			binaryWriter.Write(g_bestScore);
			binaryWriter.Write(g_bestDistance);
			binaryWriter.Write(GameCount % 3);
			binaryWriter.Write(g_bSumbit);
			binaryWriter.Close();
			fileStream.Close();
		}
		catch (IOException ex)
		{
			Debug.Log(ex.ToString());
		}
	}

	public static void ReadFile(string filename)
	{
		try
		{
			if (File.Exists(filename))
			{
				filename = getSavePath() + filename;
				Debug.Log(filename);
				FileStream fileStream = new FileStream(filename, FileMode.Open);
				BinaryReader binaryReader = new BinaryReader(fileStream);
				AllGold = binaryReader.ReadInt32();
				g_bestScore = binaryReader.ReadInt32();
				g_bestDistance = binaryReader.ReadInt32();
				GameCount = binaryReader.ReadInt32();
				g_bSumbit = binaryReader.ReadBoolean();
				binaryReader.Close();
				fileStream.Close();
			}
			else
			{
				SaveFile("3D_Run");
			}
		}
		catch (IOException ex)
		{
			Debug.Log(ex.ToString());
		}
	}
}
