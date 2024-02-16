using System.Collections;
using System.Xml;
using UnityEngine;

public class GameInfo
{
	private const string c_strCharaInfoPath = "Config/CharaInfoXML";

	private const string c_strTerrainCfg = "Config/TerrainCfgXML";

	private const string c_strItemCfg = "Config/ItemCfgXML";

	private const string c_strDeathPath = "Config/DeathXML";

	private static GameInfo m_cGameInfo;

	private bool m_bInit;

	private Hashtable m_htCharaInfo;

	private Hashtable m_htTerrain;

	private Hashtable m_htArea;

	private Hashtable m_htSubassembly;

	private Hashtable m_htMonster;

	private Hashtable m_htItem;

	private TreasureCfg m_cmTreasureBox;

	private Hashtable m_htDeath;

	private Hashtable m_htFraise;

	public static GameInfo GetInstance()
	{
		if (m_cGameInfo == null)
		{
			m_cGameInfo = new GameInfo();
		}
		return m_cGameInfo;
	}

	public void Initialize()
	{
		if (!m_bInit)
		{
			LoadCharacter();
			LoadTerrain();
			LoadItem();
			LoadDeathConfig();
			m_bInit = true;
		}
	}

	public void Destroy()
	{
	}

	private void LoadCharacter()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("Config/CharaInfoXML") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode documentElement = xmlDocument.DocumentElement;
		if (m_htCharaInfo == null)
		{
			m_htCharaInfo = new Hashtable();
		}
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if ("item" == childNode.Name)
			{
				XmlElement xmlElement = (XmlElement)childNode;
				CharaInfo charaInfo = default(CharaInfo);
				charaInfo.ID = int.Parse(xmlElement.GetAttribute("id").Trim());
				charaInfo.Name = xmlElement.GetAttribute("Name").Trim();
				charaInfo.Lift = float.Parse(xmlElement.GetAttribute("Lift").Trim());
				charaInfo.Gravity = float.Parse(xmlElement.GetAttribute("Gravity").Trim());
				charaInfo.Speed = float.Parse(xmlElement.GetAttribute("Speed").Trim());
				charaInfo.MoveEffect = xmlElement.GetAttribute("MoveEffectPath").Trim();
				charaInfo.JumpEffect = xmlElement.GetAttribute("JumpEffectPath").Trim();
				charaInfo.FireEffect = xmlElement.GetAttribute("FireEffectPath").Trim();
				charaInfo.Run = xmlElement.GetAttribute("Run").Trim();
				charaInfo.Down = xmlElement.GetAttribute("Down").Trim();
				charaInfo.Jump1 = xmlElement.GetAttribute("Jump1").Trim();
				charaInfo.Jump2 = xmlElement.GetAttribute("Jump2").Trim();
				charaInfo.JumpIdle = xmlElement.GetAttribute("JumpIdle").Trim();
				charaInfo.Left = xmlElement.GetAttribute("Left").Trim();
				charaInfo.Right = xmlElement.GetAttribute("Right").Trim();
				m_htCharaInfo.Add(charaInfo.ID, charaInfo);
			}
		}
	}

	public CharaInfo? getCharaInfoByID(int nID)
	{
		if (m_htCharaInfo == null || m_htCharaInfo[nID] == null)
		{
			return null;
		}
		return (CharaInfo)m_htCharaInfo[nID];
	}

	public void LoadTerrain()
	{
		TerrainConfig terrainConfig = Resources.Load("Config/TerrainConfig") as TerrainConfig;
		if (m_htArea == null)
		{
			m_htArea = new Hashtable();
		}
		for (int i = 0; i < terrainConfig.cmAreaList.Count; i++)
		{
			m_htArea.Add(terrainConfig.cmAreaList[i].Type, terrainConfig.cmAreaList[i]);
		}
		if (m_htTerrain == null)
		{
			m_htTerrain = new Hashtable();
		}
		for (int j = 0; j < terrainConfig.cmTerrainList.Count; j++)
		{
			m_htTerrain.Add(terrainConfig.cmTerrainList[j].Code, terrainConfig.cmTerrainList[j]);
		}
	}

	public void LoadChannels(XmlNode cmNode)
	{
		string text = null;
		int num = 0;
		foreach (XmlNode childNode in cmNode.ChildNodes)
		{
			if (!("Area" == childNode.Name))
			{
				continue;
			}
			AreaCfg areaCfg = new AreaCfg();
			XmlElement xmlElement = (XmlElement)childNode;
			areaCfg.Type = (ENUM_AREA_TYPE)(int.Parse(xmlElement.GetAttribute("Type").Trim()) - 1);
			areaCfg.MinDistance = int.Parse(xmlElement.GetAttribute("MinDistance").Trim());
			areaCfg.MaxDistance = int.Parse(xmlElement.GetAttribute("MaxDistance").Trim());
			areaCfg.Cemetery = xmlElement.GetAttribute("Cemetery").Trim();
			if (xmlElement.GetAttribute("ShowSky") != null && "True" == xmlElement.GetAttribute("ShowSky".Trim()))
			{
				areaCfg.ShowSky = true;
			}
			foreach (XmlNode childNode2 in childNode.ChildNodes)
			{
				if (!("Element" == childNode2.Name))
				{
					continue;
				}
				XmlElement xmlElement2 = (XmlElement)childNode2;
				TerrainCfg terrainCfg = new TerrainCfg();
				terrainCfg.Code = xmlElement2.GetAttribute("Code");
				terrainCfg.Area = areaCfg.Type;
				if (((XmlElement)childNode2).HasAttribute("convert") && "True" == ((XmlElement)childNode2).GetAttribute("convert").Trim())
				{
					areaCfg.addConvert(terrainCfg.Code);
				}
				if (((XmlElement)childNode2).HasAttribute("ChangeSky") && "True" == ((XmlElement)childNode2).GetAttribute("ChangeSky").Trim())
				{
					terrainCfg.ChangeSky = true;
				}
				foreach (XmlNode childNode3 in childNode2.ChildNodes)
				{
					ENUM_DIFFICULTY_TYPE nType = (ENUM_DIFFICULTY_TYPE)int.Parse(((XmlElement)childNode3).GetAttribute("Type").Trim());
					terrainCfg[nType] = int.Parse(((XmlElement)childNode3).GetAttribute("Continuation").Trim());
					foreach (XmlNode childNode4 in ((XmlElement)childNode3).ChildNodes)
					{
						text = ((XmlElement)childNode4).GetAttribute("Code").Trim();
						num = int.Parse(((XmlElement)childNode4).GetAttribute("Probability").Trim());
						if ("NextChannel" == childNode4.Name)
						{
							terrainCfg.addNextChannel(nType, text, num);
						}
						else if ("Fraise" == childNode4.Name)
						{
							terrainCfg.addFraise(nType, text, num);
						}
						else if ("NoFraise" == childNode4.Name)
						{
							terrainCfg.addNoFraise(nType, text, num);
						}
					}
				}
				m_htTerrain.Add(terrainCfg.Code, terrainCfg);
			}
			m_htArea.Add(areaCfg.Type, areaCfg);
		}
	}

	public void LoadSubassembly(XmlNode cmNode)
	{
		foreach (XmlNode childNode in cmNode.ChildNodes)
		{
			if ("Element" == childNode.Name)
			{
				XmlElement xmlElement = (XmlElement)childNode;
				SubassemblyCfg subassemblyCfg = new SubassemblyCfg();
				subassemblyCfg.Code = xmlElement.GetAttribute("Code");
				subassemblyCfg.MinSpace = int.Parse(xmlElement.GetAttribute("MinSpace").Trim());
				subassemblyCfg.Safety = int.Parse(xmlElement.GetAttribute("Safety").Trim());
				m_htSubassembly.Add(subassemblyCfg.Code, subassemblyCfg);
			}
		}
	}

	public void LoadMonster(XmlNode cmNode)
	{
		foreach (XmlNode childNode in cmNode.ChildNodes)
		{
			if ("Element" == childNode.Name)
			{
				XmlElement xmlElement = (XmlElement)childNode;
				MonsterCfg monsterCfg = new MonsterCfg();
				monsterCfg.Code = xmlElement.GetAttribute("Code");
				monsterCfg.Instance = xmlElement.GetAttribute("Instance");
				monsterCfg.MinSpace = int.Parse(xmlElement.GetAttribute("MinSpace").Trim());
				monsterCfg.Safety = int.Parse(xmlElement.GetAttribute("Safety").Trim());
				monsterCfg.MoveRange = float.Parse(xmlElement.GetAttribute("MoveRange").Trim());
				monsterCfg.MoveSpeed = float.Parse(xmlElement.GetAttribute("MoveSpeed").Trim());
				monsterCfg.AttackRange = float.Parse(xmlElement.GetAttribute("AttackRange").Trim());
				monsterCfg.AttackSpeed = float.Parse(xmlElement.GetAttribute("AttackSpeed").Trim());
				monsterCfg.HP = int.Parse(xmlElement.GetAttribute("HP").Trim());
				monsterCfg.Energy = int.Parse(xmlElement.GetAttribute("Energy").Trim());
				m_htMonster.Add(monsterCfg.Code, monsterCfg);
			}
		}
	}

	public void LoadItem()
	{
		if (m_htItem == null)
		{
			m_htItem = new Hashtable();
		}
		if (m_cmTreasureBox == null)
		{
			m_cmTreasureBox = new TreasureCfg();
		}
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("Config/ItemCfgXML") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode documentElement = xmlDocument.DocumentElement;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if ("Item" == childNode.Name)
			{
				LoadItemInfo(childNode);
			}
			else if ("TreasureBox" == childNode.Name)
			{
				LoadTreasureBox(childNode);
			}
		}
	}

	public void LoadItemInfo(XmlNode cmNode)
	{
		int num = 0;
		foreach (XmlNode item in cmNode)
		{
			if (!("Element" == item.Name))
			{
				continue;
			}
			XmlElement xmlElement = (XmlElement)item;
			ItemCfg itemCfg = new ItemCfg();
			itemCfg.Code = xmlElement.GetAttribute("Code");
			itemCfg.ItemType = (ENUM_ITEM_TYPE)int.Parse(xmlElement.GetAttribute("Type").Trim());
			itemCfg.Time = int.Parse(xmlElement.GetAttribute("Time").Trim());
			itemCfg.OnceEffect = xmlElement.GetAttribute("OnceEffect").Trim();
			itemCfg.BufferCount = item.ChildNodes.Count;
			num = 0;
			foreach (XmlNode childNode in item.ChildNodes)
			{
				if (!("#comment" == childNode.Name))
				{
					XmlElement xmlElement2 = (XmlElement)childNode;
					itemCfg.setBufferType(num, (ENUM_CHARA_BUFFER)int.Parse(((XmlElement)childNode).GetAttribute("Type").Trim()));
					itemCfg.setBufferStength(num, int.Parse(((XmlElement)childNode).GetAttribute("Stength").Trim()));
					itemCfg.setBufferEffect(num, ((XmlElement)childNode).GetAttribute("Effect").Trim());
					itemCfg.setBufferStartAudio(num, ((XmlElement)childNode).GetAttribute("StartAudio").Trim());
					itemCfg.setBufferEndAudio(num, ((XmlElement)childNode).GetAttribute("EndAudio").Trim());
					num++;
				}
			}
			m_htItem.Add(itemCfg.Code, itemCfg);
		}
	}

	public void LoadTreasureBox(XmlNode cmNode)
	{
		if (m_cmTreasureBox == null)
		{
			m_cmTreasureBox = new TreasureCfg();
		}
		int num = 0;
		foreach (XmlNode item in cmNode)
		{
			if ("Item" == item.Name)
			{
				XmlElement xmlElement = (XmlElement)item;
				string attribute = xmlElement.GetAttribute("Code");
				int result;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv1").Trim(), out result);
				int result2;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv2").Trim(), out result2);
				int result3;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv3").Trim(), out result3);
				int result4;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv4").Trim(), out result4);
				int result5;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv5").Trim(), out result5);
				int result6;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv6").Trim(), out result6);
				int result7;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv7").Trim(), out result7);
				int result8;
				int.TryParse(xmlElement.GetAttribute("DifficultyLv8").Trim(), out result8);
				string attribute2 = xmlElement.GetAttribute("Effect");
				m_cmTreasureBox.addProbability(attribute, result, result2, result3, result4, result5, result6, result7, result8, attribute2);
				Debug.Log("LoadTreasureBox strCode = " + attribute);
			}
		}
	}

	public string checkArea(ENUM_AREA_TYPE nType, int nDistance)
	{
		if (ENUM_AREA_TYPE.MAX_AREA <= nType)
		{
			return null;
		}
		if (m_htArea == null || m_htArea[nType] == null)
		{
			return null;
		}
		AreaCfg areaCfg = (AreaCfg)m_htArea[nType];
		return areaCfg.getConvert(nDistance);
	}

	public string getAreaCemetery(ENUM_AREA_TYPE nType)
	{
		if (ENUM_AREA_TYPE.MAX_AREA <= nType)
		{
			return null;
		}
		if (m_htArea == null || m_htArea[nType] == null)
		{
			return null;
		}
		AreaCfg areaCfg = (AreaCfg)m_htArea[nType];
		return areaCfg.Cemetery;
	}

	public bool getAreaSky(ENUM_AREA_TYPE nType)
	{
		if (ENUM_AREA_TYPE.MAX_AREA <= nType)
		{
			return false;
		}
		if (m_htArea == null || m_htArea[nType] == null)
		{
			return false;
		}
		AreaCfg areaCfg = (AreaCfg)m_htArea[nType];
		return areaCfg.ShowSky;
	}

	public TerrainCfg getTerrainCfg(string strCode)
	{
		if (m_htTerrain == null || strCode == null)
		{
			return null;
		}
		if (m_htTerrain[strCode] == null)
		{
			foreach (object key in m_htTerrain.Keys)
			{
				if (!(key.ToString() == strCode))
				{
				}
			}
			return null;
		}
		return (TerrainCfg)m_htTerrain[strCode];
	}

	public string[] getAllTerrainKey()
	{
		if (m_htTerrain == null)
		{
			return null;
		}
		string[] array = new string[m_htTerrain.Count];
		m_htTerrain.Keys.CopyTo(array, 0);
		return array;
	}

	public SubassemblyCfg getSubassemblyCfg(string strCode)
	{
		if (m_htSubassembly == null)
		{
			return null;
		}
		if (m_htSubassembly[strCode] == null)
		{
			Debug.Log("Not Found Subassembly = " + strCode);
			return null;
		}
		return (SubassemblyCfg)m_htSubassembly[strCode];
	}

	public string[] getAllSubassemblyKey()
	{
		if (m_htSubassembly == null)
		{
			return null;
		}
		string[] array = new string[m_htSubassembly.Count];
		m_htSubassembly.Keys.CopyTo(array, 0);
		return array;
	}

	public ENUM_ITEM_TYPE getItemType(string strCode)
	{
		if (m_htItem == null)
		{
			return ENUM_ITEM_TYPE.ITEM_NONE;
		}
		if (m_htItem[strCode] == null)
		{
			return ENUM_ITEM_TYPE.ITEM_NONE;
		}
		return ((ItemCfg)m_htItem[strCode]).ItemType;
	}

	public ItemCfg getItemCfg(string strCode)
	{
		if (m_htItem == null)
		{
			return null;
		}
		if (m_htItem[strCode] == null)
		{
			Debug.Log("Not Found Item = " + strCode);
			return null;
		}
		return (ItemCfg)m_htItem[strCode];
	}

	public bool isItem(string strCode)
	{
		if (m_htItem == null)
		{
			return false;
		}
		if (m_htItem[strCode] == null)
		{
			return false;
		}
		return true;
	}

	public string[] getAllItemKey()
	{
		if (m_htItem == null)
		{
			return null;
		}
		string[] array = new string[m_htItem.Count];
		m_htItem.Keys.CopyTo(array, 0);
		return array;
	}

	public string getTreasureBoxItem()
	{
		return m_cmTreasureBox.getItem(globalVal.RandomType);
	}

	public string getTreasureBoxEffect(string strCode)
	{
		return m_cmTreasureBox.getEffect(strCode);
	}

	public MonsterCfg getMonsterCfg(string strCode)
	{
		if (m_htMonster == null)
		{
			return null;
		}
		if (m_htMonster[strCode] == null)
		{
			return null;
		}
		return (MonsterCfg)m_htMonster[strCode];
	}

	public string[] getAllMonsterKey()
	{
		if (m_htMonster == null)
		{
			return null;
		}
		string[] array = new string[m_htMonster.Count];
		m_htMonster.Keys.CopyTo(array, 0);
		return array;
	}

	public void LoadDeathConfig()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("Config/DeathXML") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode documentElement = xmlDocument.DocumentElement;
		DeathCfg deathCfg = null;
		if (m_htDeath == null)
		{
			m_htDeath = new Hashtable();
		}
		if (m_htFraise == null)
		{
			m_htFraise = new Hashtable();
		}
		XmlElement xmlElement = null;
		int num = 0;
		bool flag = false;
		string empty = string.Empty;
		string empty2 = string.Empty;
		string empty3 = string.Empty;
		foreach (XmlNode childNode in documentElement.ChildNodes)
		{
			if (!("type" == childNode.Name))
			{
				continue;
			}
			xmlElement = (XmlElement)childNode;
			num = int.Parse(xmlElement.GetAttribute("id").Trim());
			empty = xmlElement.GetAttribute("Animation").Trim();
			foreach (XmlNode childNode2 in xmlElement.ChildNodes)
			{
				flag = false;
				if ("Fraise" == childNode2.Name)
				{
					string text = ((XmlElement)childNode2).GetAttribute("Code").Trim();
					if (((XmlElement)childNode2).GetAttribute("isBody") != null)
					{
						flag = "true" == ((XmlElement)childNode2).GetAttribute("isBody").Trim().ToLower();
					}
					empty3 = (text = ((XmlElement)childNode2).GetAttribute("Code").Trim());
					deathCfg = (DeathCfg)m_htFraise[text];
					if (deathCfg == null)
					{
						deathCfg = new DeathCfg();
						m_htFraise.Add(text, deathCfg);
					}
					if (flag)
					{
						deathCfg.Death1 = empty;
						deathCfg.FraiseAnimation1 = empty3;
					}
					else
					{
						deathCfg.Death2 = empty;
						deathCfg.FraiseAnimation2 = empty3;
					}
					m_htFraise[text] = deathCfg;
				}
			}
			m_htDeath.Add(num, empty2);
		}
	}

	public DeathCfg getDeath(string strCollider)
	{
		if (m_htFraise == null || strCollider == null || m_htFraise[strCollider] == null)
		{
			return null;
		}
		return m_htFraise[strCollider] as DeathCfg;
	}

	public string getDeathPic(int nType)
	{
		if (m_htDeath == null)
		{
			return null;
		}
		return m_htDeath[nType] as string;
	}

	public int getDeathType(string strCollider)
	{
		if (m_htFraise == null || m_htDeath == null || strCollider == null || m_htFraise[strCollider] == null)
		{
			return -1;
		}
		int num = (int)m_htFraise[strCollider];
		if (m_htDeath[num] == null)
		{
			return -1;
		}
		return num;
	}

	public int getDeathCount()
	{
		if (m_htDeath == null)
		{
			return 0;
		}
		return m_htDeath.Count;
	}
}
