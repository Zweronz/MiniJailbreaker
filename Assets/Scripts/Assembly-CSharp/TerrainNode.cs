using System.Collections;
using UnityEngine;

public class TerrainNode
{
	private Transform m_cmChannel;

	private TerrainNode m_cmNext;

	private ArrayList m_alSubList;

	private ArrayList m_alMonster;

	private GameTerrain m_cmTerrain;

	public Transform Channel
	{
		get
		{
			return m_cmChannel;
		}
		set
		{
			m_cmChannel = value;
			m_cmTerrain = m_cmChannel.GetComponent(typeof(GameTerrain)) as GameTerrain;
		}
	}

	public TerrainNode Next
	{
		get
		{
			return m_cmNext;
		}
		set
		{
			m_cmNext = value;
		}
	}

	public GameTerrain Terrain
	{
		get
		{
			return m_cmTerrain;
		}
	}

	public void addSub(Transform cmSub)
	{
		if (m_alSubList == null)
		{
			m_alSubList = new ArrayList();
		}
		if (null != m_cmTerrain.SubList)
		{
			cmSub.parent = m_cmTerrain.SubList.parent;
		}
		m_alSubList.Add(cmSub);
	}

	public void addMonster(Transform cmMonster)
	{
		if (m_alMonster == null)
		{
			m_alMonster = new ArrayList();
		}
		if (null != m_cmTerrain.MonsterList)
		{
			cmMonster.parent = m_cmTerrain.MonsterList.parent;
		}
		m_alMonster.Add(cmMonster);
	}

	public void clear()
	{
		globalVal.Channel.recycleChannel(m_cmChannel);
		m_cmChannel = null;
		if (m_alSubList != null)
		{
			for (int i = 0; i < m_alSubList.Count; i++)
			{
				globalVal.Subassembly.recycleSubassembly((Transform)m_alSubList[i]);
			}
			m_alSubList.Clear();
			m_alSubList = null;
		}
		if (null != m_cmTerrain)
		{
			m_cmTerrain.recycle();
		}
	}
}
