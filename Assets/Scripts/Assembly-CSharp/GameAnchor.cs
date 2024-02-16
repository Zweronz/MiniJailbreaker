using System.Collections;
using UnityEngine;

public class GameAnchor
{
	private Vector3 m_v3Anchor;

	private AnchorInfo m_cmStart;

	private float[] m_alInterval;

	private Transform m_cmSubList;

	private Transform m_cmMonsterList;

	private ArrayList m_alWayPoint;

	public void Initialize()
	{
		m_v3Anchor = Vector3.zero;
		m_alInterval = new float[4];
		for (int i = 0; i < 4; i++)
		{
			m_alInterval[i] = 50f;
		}
	}

	public void Reset()
	{
		clearWayPoint();
		Initialize();
	}

	public void Destroy()
	{
	}

	public void clearWayPoint()
	{
		if (m_alWayPoint != null)
		{
			m_alWayPoint.Clear();
			m_alWayPoint = null;
		}
	}

	public void addWayPoint(Vector3 v3Start, Vector3 v3End, Transform cmSubList, Transform cmMonsterList, TerrainCfg cmCfg, TerrainNode cmNode)
	{
		if (m_alWayPoint == null)
		{
			m_alWayPoint = new ArrayList();
		}
		AnchorInfo anchorInfo = new AnchorInfo();
		anchorInfo.StartPoint = v3Start;
		anchorInfo.EndPoint = v3End;
		anchorInfo.SubList = cmSubList;
		anchorInfo.MonsterList = cmMonsterList;
		anchorInfo.Config = cmCfg;
		anchorInfo.Node = cmNode;
		m_alWayPoint.Add(anchorInfo);
	}

	private AnchorInfo getWayPoint()
	{
		if (m_alWayPoint == null || m_alWayPoint.Count == 0)
		{
			return null;
		}
		AnchorInfo result = (AnchorInfo)m_alWayPoint[0];
		m_alWayPoint.RemoveAt(0);
		return result;
	}

	public void Run()
	{
		AnchorInfo wayPoint = getWayPoint();
		if (wayPoint != null)
		{
			Vector3 v3Forward = Vector3.Normalize(wayPoint.EndPoint - wayPoint.StartPoint);
			Vector3 startPoint = wayPoint.StartPoint;
			if (null == wayPoint.SubList)
			{
				popSub(startPoint, wayPoint.EndPoint, v3Forward, wayPoint.Config, wayPoint.Node);
			}
			else
			{
				setSub(wayPoint.SubList, wayPoint.Node);
			}
			if (null == wayPoint.MonsterList)
			{
				popMonster(startPoint, wayPoint.EndPoint, v3Forward, wayPoint.Config, wayPoint.Node);
			}
			else
			{
				setMonster(wayPoint.MonsterList, wayPoint.Node);
			}
		}
	}

	public void popAnchor(Vector3 v3Anchor, Vector3 v3Forward, TerrainCfg cmCfg, ENUM_INTERVAL_TYPE nType, TerrainNode cmNode)
	{
		if (cmCfg == null)
		{
			return;
		}
		SubassemblyCfg subassemblyCfg = null;
		switch (nType)
		{
		case ENUM_INTERVAL_TYPE.FARISE:
		{
			string noFraise = cmCfg.getFraise(globalVal.RandomType);
			if (noFraise != null)
			{
				subassemblyCfg = globalVal.Config.getSubassemblyCfg(noFraise);
			}
			break;
		}
		case ENUM_INTERVAL_TYPE.ITEM:
		{
			string noFraise = cmCfg.getNoFraise(globalVal.RandomType);
			if (noFraise != null)
			{
				subassemblyCfg = globalVal.Config.getSubassemblyCfg(noFraise);
			}
			break;
		}
		}
		if (subassemblyCfg == null)
		{
			m_alInterval[0] = 10f;
			m_alInterval[(int)nType] = 10f;
		}
		else
		{
			m_alInterval[0] = subassemblyCfg.Safety;
			m_alInterval[(int)nType] = subassemblyCfg.MinSpace;
		}
	}

	public void popSub(Vector3 v3Anchor, Vector3 v3End, Vector3 v3Forward, TerrainCfg cmCfg, TerrainNode cmNode)
	{
		if (cmCfg == null)
		{
			return;
		}
		float num = 0f;
		ENUM_INTERVAL_TYPE eNUM_INTERVAL_TYPE = ENUM_INTERVAL_TYPE.MAX_INTERVAL;
		ENUM_INTERVAL_TYPE eNUM_INTERVAL_TYPE2 = ENUM_INTERVAL_TYPE.MAX_INTERVAL;
		while (true)
		{
			eNUM_INTERVAL_TYPE = ((m_alInterval[1] < m_alInterval[2]) ? ENUM_INTERVAL_TYPE.FARISE : ENUM_INTERVAL_TYPE.ITEM);
			eNUM_INTERVAL_TYPE2 = ((!(m_alInterval[0] > m_alInterval[(int)eNUM_INTERVAL_TYPE])) ? eNUM_INTERVAL_TYPE : ENUM_INTERVAL_TYPE.SAFETY);
			num = Vector3.Distance(v3Anchor, v3End);
			if (m_alInterval[(int)eNUM_INTERVAL_TYPE2] > num)
			{
				break;
			}
			v3Anchor += m_alInterval[(int)eNUM_INTERVAL_TYPE2] * v3Forward;
			for (int i = 0; i < 3; i++)
			{
				m_alInterval[i] -= m_alInterval[(int)eNUM_INTERVAL_TYPE2];
			}
			popAnchor(v3Anchor, v3Forward, cmCfg, eNUM_INTERVAL_TYPE, cmNode);
		}
		for (int j = 0; j < 3; j++)
		{
			m_alInterval[j] -= num;
		}
	}

	public void setSub(Transform cmSubList, TerrainNode cmNode)
	{
		if (!(m_cmSubList == cmSubList))
		{
			m_cmSubList = cmSubList;
			Transform transform = null;
			for (int i = 0; i < cmSubList.childCount; i++)
			{
				transform = cmSubList.GetChild(i);
				GameFixList.GetInstance().addFixNode(transform.name, cmNode, 0f, transform.position, Vector3.zero, transform.eulerAngles, false);
			}
		}
	}

	public void popMonster(Vector3 v3Anchor, Vector3 v3End, Vector3 v3Forward, TerrainCfg cmCfg, TerrainNode cmNode)
	{
		if (cmCfg == null)
		{
			return;
		}
		float num = 0f;
		while (true)
		{
			num = Vector3.Distance(v3Anchor, v3End);
			if (m_alInterval[3] > num)
			{
				break;
			}
			v3Anchor += m_alInterval[3] * v3Forward;
			m_alInterval[3] = 0f;
			popAnchor(v3Anchor, -v3Forward, cmCfg, ENUM_INTERVAL_TYPE.MONSTER, cmNode);
		}
		m_alInterval[3] -= num;
	}

	public void setMonster(Transform cmMonsterList, TerrainNode cmNode)
	{
		if (!(m_cmMonsterList == cmMonsterList))
		{
			m_cmMonsterList = cmMonsterList;
			Transform transform = null;
			for (int i = 0; i < cmMonsterList.childCount; i++)
			{
				transform = cmMonsterList.GetChild(i);
				GameFixList.GetInstance().addFixNode(transform.name, cmNode, 0f, transform.position, Vector3.zero, transform.eulerAngles, true);
			}
		}
	}
}
