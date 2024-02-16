using UnityEngine;

public class AnchorInfo
{
	private Vector3 m_v3StartPoint;

	private Vector3 m_v3EndPoint;

	private Transform m_cmSubList;

	private Transform m_cmMonsterList;

	private TerrainCfg m_cmCfg;

	private TerrainNode m_cmNode;

	public Vector3 StartPoint
	{
		get
		{
			return m_v3StartPoint;
		}
		set
		{
			m_v3StartPoint = value;
		}
	}

	public Vector3 EndPoint
	{
		get
		{
			return m_v3EndPoint;
		}
		set
		{
			m_v3EndPoint = value;
		}
	}

	public Transform SubList
	{
		get
		{
			return m_cmSubList;
		}
		set
		{
			m_cmSubList = value;
		}
	}

	public Transform MonsterList
	{
		get
		{
			return m_cmMonsterList;
		}
		set
		{
			m_cmMonsterList = value;
		}
	}

	public TerrainCfg Config
	{
		get
		{
			return m_cmCfg;
		}
		set
		{
			m_cmCfg = value;
		}
	}

	public TerrainNode Node
	{
		get
		{
			return m_cmNode;
		}
		set
		{
			m_cmNode = value;
		}
	}
}
