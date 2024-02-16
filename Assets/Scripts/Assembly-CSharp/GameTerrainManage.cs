using UnityEngine;

public class GameTerrainManage
{
	private const string c_strChannelName = "Start";

	private const int c_nCreateDistance = 300;

	private const int c_nDestroyDistance = 50;

	private const string c_strStartAnimation = "01";

	private static GameTerrainManage m_cmInstance;

	private TerrainNode m_cmHead;

	private TerrainNode m_cmTail;

	private int m_nContinuation;

	private int m_nAreaDistance;

	private Transform m_cmPlayer;

	private GameAnchor m_cmAnchor;

	private float m_fTime;

	private Transform m_cmStart;

	private bool m_bPlayStart;

	public Transform Player
	{
		set
		{
			m_cmPlayer = value;
		}
	}

	private void Start()
	{
		m_cmStart = GameObject.Find("Start").transform;
	}

	public void Run()
	{
		if (Time.realtimeSinceStartup - m_fTime < 0.5f)
		{
			return;
		}
		m_fTime = Time.realtimeSinceStartup;
		if (null == m_cmPlayer)
		{
			return;
		}
		if (m_bPlayStart && null != m_cmStart && 30f < m_cmPlayer.position.z)
		{
			m_cmStart.gameObject.SetActiveRecursively(false);
			m_bPlayStart = false;
		}
		Vector3 vector = Vector3.zero;
		if (m_cmTail == m_cmHead || m_cmTail == m_cmHead.Next)
		{
			addTerrain();
			return;
		}
		if (m_cmTail != null && null != m_cmTail.Terrain && m_cmTail.Terrain.Tail != null && null != m_cmTail.Terrain.Tail.WayPoint)
		{
			vector = m_cmTail.Terrain.Tail.WayPoint.position;
		}
		if (300f > Vector3.Distance(vector, m_cmPlayer.position))
		{
			addTerrain();
			return;
		}
		if (m_cmHead != null && null != m_cmHead.Terrain && m_cmHead.Terrain.Tail != null && null != m_cmHead.Terrain.Tail.WayPoint)
		{
			vector = m_cmHead.Terrain.Tail.WayPoint.position;
		}
		if (!(90f <= Vector3.Angle(m_cmPlayer.forward, Vector3.Normalize(m_cmPlayer.position - vector))) && 50f <= Vector3.Distance(vector, m_cmPlayer.position))
		{
			destroyTerrain();
		}
	}

	public static GameTerrainManage GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameTerrainManage();
		}
		return m_cmInstance;
	}

	public void Initialize()
	{
		m_cmAnchor = new GameAnchor();
		m_cmAnchor.Initialize();
		m_cmHead = null;
		m_cmTail = null;
		m_nContinuation = 0;
		m_nAreaDistance = 0;
		PlayStart();
	}

	public void Destroy()
	{
		m_cmHead = null;
		m_cmTail = null;
	}

	public void Reset()
	{
		while (m_cmHead != null)
		{
			TerrainNode cmHead = m_cmHead;
			m_cmHead = m_cmHead.Next;
			cmHead.clear();
		}
		m_cmTail = null;
		m_cmHead = null;
		m_cmAnchor.Reset();
		m_nContinuation = 0;
		m_nAreaDistance = 0;
		PlayStart();
	}

	public void PlayStart()
	{
		if (!(null == m_cmStart))
		{
			m_cmStart.gameObject.SetActiveRecursively(true);
			m_bPlayStart = true;
		}
	}

	public void addTerrain()
	{
		string text = getNextChannelCode();
		if (text == null || 0 >= text.Length)
		{
			text = "connect_ne";
		}
		Transform channel = globalVal.Channel.getChannel(text);
		if (null == channel)
		{
			Debug.Log("cmChannel is null! strCode = " + text);
			channel = globalVal.Channel.getChannel("connect_ne");
		}
		GameTerrain gameTerrain = channel.GetComponent(typeof(GameTerrain)) as GameTerrain;
		if (!(null == gameTerrain))
		{
			gameTerrain.Initialize();
			m_nAreaDistance += Mathf.FloorToInt(gameTerrain.Distance);
			linkTerrain(channel, gameTerrain.Head.WayPoint);
			updateNode(channel, gameTerrain);
			addPoint2Anchor(m_cmTail, gameTerrain);
			Subway subway = channel.GetComponent(typeof(Subway)) as Subway;
			if (null != subway)
			{
				subway.shootTrain(gameTerrain.Tail);
			}
			m_cmAnchor.Run();
		}
	}

	private string getNextChannelCode()
	{
		TerrainCfg terrainCfg = null;
		GameTerrain gameTerrain = null;
		if (m_cmTail != null && null != m_cmTail.Channel)
		{
			gameTerrain = m_cmTail.Channel.GetComponent(typeof(GameTerrain)) as GameTerrain;
			if (null != gameTerrain)
			{
				terrainCfg = gameTerrain.Config;
			}
		}
		string text = null;
		if (terrainCfg != null)
		{
			text = globalVal.Config.checkArea(terrainCfg.Area, m_nAreaDistance);
		}
		if (text != null)
		{
			m_nContinuation = 0;
			m_nAreaDistance = 0;
		}
		if (1 < m_nContinuation)
		{
			if (terrainCfg != null)
			{
				text = terrainCfg.Code;
			}
			m_nContinuation--;
		}
		if (text == null)
		{
			if (terrainCfg == null)
			{
				terrainCfg = globalVal.Config.getTerrainCfg("Start");
			}
			if (terrainCfg != null)
			{
				text = terrainCfg.getNextChannel(globalVal.RandomType);
				if (text != terrainCfg.Code)
				{
					terrainCfg = globalVal.Config.getTerrainCfg(text);
					if (terrainCfg == null)
					{
						terrainCfg = globalVal.Config.getTerrainCfg("road_general_1-3_01");
						if (terrainCfg == null)
						{
							Debug.Log("Not Found Terrain Cfg = " + text + " CurChannel = " + gameTerrain.name);
							return null;
						}
					}
					m_nContinuation = terrainCfg[globalVal.RandomType];
				}
			}
		}
		return text;
	}

	private void linkTerrain(Transform cmChannel, Transform cmPoint)
	{
		if (!(null == cmPoint) && !(null == cmChannel))
		{
			Transform transform = null;
			Transform transform2 = null;
			cmChannel.position = Vector3.zero;
			if (m_cmTail != null && null != m_cmTail.Channel && null != m_cmTail.Terrain)
			{
				transform = m_cmTail.Channel;
				transform2 = m_cmTail.Terrain.Tail.WayPoint;
			}
			float num = 0f;
			float num2 = 0f;
			if (null != transform2)
			{
				num = transform.eulerAngles.y + transform2.localEulerAngles.y;
				num2 = transform.eulerAngles.x + transform2.localEulerAngles.x;
			}
			num += 360f - cmPoint.localEulerAngles.y;
			num2 += cmPoint.localEulerAngles.x;
			cmChannel.Rotate(Vector3.up, num);
			cmChannel.Rotate(Vector3.right, num2);
			if (null == transform2)
			{
				cmChannel.position = Vector3.zero - cmPoint.position;
			}
			else
			{
				cmChannel.position = transform2.position - cmPoint.position;
			}
		}
	}

	private void updateNode(Transform cmChannel, GameTerrain cmTerrain)
	{
		if (m_cmHead == null)
		{
			m_cmHead = new TerrainNode();
			m_cmHead.Channel = cmChannel;
			m_cmTail = m_cmHead;
		}
		else if (m_cmTail != null)
		{
			m_cmTail.Terrain.Tail.NextPoint = cmTerrain.Head;
			cmTerrain.Head.PrePoint = m_cmTail.Terrain.Tail;
			m_cmTail.Next = new TerrainNode();
			m_cmTail = m_cmTail.Next;
			m_cmTail.Channel = cmChannel;
		}
	}

	private void addPoint2Anchor(TerrainNode cmNode, GameTerrain cmTerrain)
	{
		if (!(null == cmTerrain))
		{
			WayPointNode wayPointNode = cmTerrain.Head;
			while (wayPointNode != null && null != wayPointNode.WayPoint && wayPointNode.NextPoint != null && null != wayPointNode.NextPoint.WayPoint)
			{
				m_cmAnchor.addWayPoint(wayPointNode.WayPoint.position, wayPointNode.NextPoint.WayPoint.position, cmTerrain.SubList, cmTerrain.MonsterList, cmTerrain.Config, cmNode);
				wayPointNode = wayPointNode.NextPoint;
			}
		}
	}

	public void destroyTerrain()
	{
		if (m_cmHead != null)
		{
			TerrainNode cmHead = m_cmHead;
			m_cmHead = m_cmHead.Next;
			cmHead.clear();
		}
	}

	public WayPointNode getHead()
	{
		if (m_cmHead == null)
		{
			return null;
		}
		return m_cmHead.Terrain.Head;
	}

	public WayPointNode addCemetery()
	{
		if (null != m_cmStart && m_cmStart.gameObject.activeInHierarchy)
		{
			m_cmStart.GetComponent<Animation>().Stop();
			m_cmStart.gameObject.SetActiveRecursively(false);
		}
		string areaCemetery = globalVal.Config.getAreaCemetery(m_cmHead.Terrain.Head.Area);
		Transform channel = globalVal.Channel.getChannel(areaCemetery);
		if (null == channel)
		{
			return null;
		}
		TerrainNode terrainNode = new TerrainNode();
		terrainNode.Channel = channel;
		terrainNode.Terrain.Initialize();
		terrainNode.Next = m_cmHead;
		m_cmHead = terrainNode;
		m_cmHead.Terrain.Tail.NextPoint = m_cmHead.Next.Terrain.Head;
		m_cmHead.Next.Terrain.Head.PrePoint = m_cmHead.Terrain.Tail;
		m_cmHead.Channel.localEulerAngles = Vector3.zero;
		m_cmHead.Channel.position = Vector3.zero;
		float num = m_cmHead.Next.Channel.localEulerAngles.y + m_cmHead.Channel.localEulerAngles.y;
		num += 360f - m_cmHead.Terrain.Tail.WayPoint.localEulerAngles.y;
		m_cmHead.Channel.Rotate(Vector3.up, num);
		Debug.Log(string.Concat("m_cmHead.Point = ", m_cmHead.Terrain.Tail.WayPoint.position, " m_cmHead.Next.Point = ", m_cmHead.Next.Terrain.Head.WayPoint.position));
		m_cmHead.Channel.position = m_cmHead.Next.Terrain.Head.WayPoint.position - m_cmHead.Terrain.Tail.WayPoint.position;
		return terrainNode.Terrain.Head;
	}
}
