using UnityEngine;

[ExecuteInEditMode]
public class GameTerrain : MonoBehaviour
{
	private WayPointNode m_cmHead;

	private WayPointNode m_cmTail;

	private float m_fDistance;

	private TerrainCfg m_cmConfig;

	private bool m_bInitialize;

	private ENUM_SHOW_SEARCHLIGHT m_nSearch;

	public Transform SubList;

	public Transform MonsterList;

	public WayPointNode Head
	{
		get
		{
			return m_cmHead;
		}
	}

	public WayPointNode Tail
	{
		get
		{
			return m_cmTail;
		}
	}

	public float Distance
	{
		get
		{
			return m_fDistance;
		}
	}

	public TerrainCfg Config
	{
		get
		{
			return m_cmConfig;
		}
	}

	public ENUM_SHOW_SEARCHLIGHT SearchLight
	{
		get
		{
			return m_nSearch;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Initialize()
	{
		if (m_cmConfig != null && m_cmConfig.ChangeSky)
		{
			GameObject gameObject = GameObject.Find("Charasky(Clone)/sky");
			if (null != gameObject)
			{
				SkyBox skyBox = gameObject.transform.GetComponent(typeof(SkyBox)) as SkyBox;
				skyBox.ChangeSky();
			}
		}
		if (m_bInitialize)
		{
			return;
		}
		m_cmConfig = globalVal.Config.getTerrainCfg(base.transform.name);
		if (m_cmConfig == null)
		{
			Debug.Log("transform = " + base.transform.name);
		}
		Transform transform = base.transform.Find("WayPoint");
		if (null == transform)
		{
			return;
		}
		Transform transform2 = null;
		Transform[] array = new Transform[transform.childCount];
		int num = -1;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			num = -1;
			int.TryParse(transform2.name, out num);
			if (num == -1 || num >= array.Length)
			{
				Debug.Log("nIndex is error ! cmPoint = " + transform2.name + " Terrain = " + base.transform.name);
			}
			else
			{
				array[num] = transform2;
			}
		}
		string text = base.transform.name;
		m_nSearch = ENUM_SHOW_SEARCHLIGHT.SEARCHLIGHT_SHOW_NORMAL;
		if (text.IndexOf("_un") != -1)
		{
			m_nSearch = ENUM_SHOW_SEARCHLIGHT.SEARCHLIGHT_SHOW_UN;
		}
		else if (text.IndexOf("_ne") != -1)
		{
			m_nSearch = ENUM_SHOW_SEARCHLIGHT.SEARCHLIGHT_SHOW_NE;
		}
		m_fDistance = 0f;
		WayPointNode wayPointNode = (m_cmHead = null);
		for (int j = 0; j < array.Length; j++)
		{
			transform2 = array[j];
			if (!(null == transform2))
			{
				if (wayPointNode == null)
				{
					wayPointNode = new WayPointNode();
				}
				if (m_cmHead == null)
				{
					m_cmHead = wayPointNode;
				}
				wayPointNode.WayPoint = transform2;
				if (wayPointNode.PrePoint != null && null != wayPointNode.PrePoint.WayPoint)
				{
					m_fDistance += Vector3.Distance(wayPointNode.PrePoint.WayPoint.position, wayPointNode.WayPoint.position);
				}
				if (m_cmConfig != null)
				{
					wayPointNode.Area = m_cmConfig.Area;
				}
				wayPointNode.Name = text;
				wayPointNode.SearchLight = m_nSearch;
				wayPointNode.NextPoint = new WayPointNode();
				wayPointNode.NextPoint.PrePoint = wayPointNode;
				wayPointNode = wayPointNode.NextPoint;
			}
		}
		m_cmTail = wayPointNode.PrePoint;
		m_cmTail.NextPoint = null;
		wayPointNode = null;
		m_bInitialize = true;
	}

	public void recycle()
	{
		m_cmHead.PrePoint = null;
		m_cmTail.NextPoint = null;
	}
}
