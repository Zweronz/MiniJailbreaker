using System;
using UnityEngine;

[Serializable]
public class TerrainCfg
{
	public string m_strCode;

	public int[] m_nContinuation;

	public ENUM_AREA_TYPE m_nArea;

	public bool m_bChangeSky;

	public Difficulty[] m_cmDifficulty;

	public string Code
	{
		get
		{
			return m_strCode;
		}
		set
		{
			m_strCode = value;
		}
	}

	public ENUM_AREA_TYPE Area
	{
		get
		{
			return m_nArea;
		}
		set
		{
			m_nArea = value;
		}
	}

	public int this[ENUM_DIFFICULTY_TYPE nType]
	{
		get
		{
			if (m_nContinuation == null)
			{
				return 0;
			}
			return m_nContinuation[(int)nType];
		}
		set
		{
			if (m_nContinuation == null)
			{
				m_nContinuation = new int[8];
			}
			m_nContinuation[(int)(nType - 1)] = value;
		}
	}

	public bool ChangeSky
	{
		get
		{
			return m_bChangeSky;
		}
		set
		{
			m_bChangeSky = value;
		}
	}

	public TerrainCfg()
	{
		m_strCode = null;
		m_nContinuation = new int[8];
		m_nArea = ENUM_AREA_TYPE.MAX_AREA;
		m_bChangeSky = false;
		m_cmDifficulty = new Difficulty[8];
	}

	public void addNextChannel(ENUM_DIFFICULTY_TYPE nType, string strCode, int nProbability)
	{
		if (m_cmDifficulty == null)
		{
			m_cmDifficulty = new Difficulty[8];
		}
		if (m_cmDifficulty[(int)(nType - 1)] == null)
		{
			m_cmDifficulty[(int)(nType - 1)] = new Difficulty();
		}
		m_cmDifficulty[(int)(nType - 1)].NextChannel.addElement(strCode, nProbability);
	}

	public string getNextChannel(ENUM_DIFFICULTY_TYPE nType)
	{
		if (m_cmDifficulty == null)
		{
			Debug.Log("m_cmDifficulty is null!!!");
			return null;
		}
		if (m_cmDifficulty[(int)nType] == null)
		{
			Debug.Log("m_cmDifficulty[nType] is null!!!" + nType);
			return null;
		}
		return m_cmDifficulty[(int)nType].NextChannel.getRandomElement();
	}

	public void addFraise(ENUM_DIFFICULTY_TYPE nType, string strCode, int nProbability)
	{
		if (m_cmDifficulty == null)
		{
			m_cmDifficulty = new Difficulty[8];
		}
		if (m_cmDifficulty[(int)(nType - 1)] == null)
		{
			m_cmDifficulty[(int)(nType - 1)] = new Difficulty();
		}
		m_cmDifficulty[(int)(nType - 1)].Fraise.addElement(strCode, nProbability);
	}

	public string getFraise(ENUM_DIFFICULTY_TYPE nType)
	{
		if (m_cmDifficulty == null)
		{
			return null;
		}
		if (m_cmDifficulty[(int)nType] == null)
		{
			return null;
		}
		return m_cmDifficulty[(int)nType].Fraise.getRandomElement();
	}

	public void addNoFraise(ENUM_DIFFICULTY_TYPE nType, string strCode, int nProbability)
	{
		if (m_cmDifficulty == null)
		{
			m_cmDifficulty = new Difficulty[8];
		}
		if (m_cmDifficulty[(int)(nType - 1)] == null)
		{
			m_cmDifficulty[(int)(nType - 1)] = new Difficulty();
		}
		m_cmDifficulty[(int)(nType - 1)].NoFraise.addElement(strCode, nProbability);
	}

	public string getNoFraise(ENUM_DIFFICULTY_TYPE nType)
	{
		if (m_cmDifficulty == null)
		{
			return null;
		}
		if (m_cmDifficulty[(int)nType] == null)
		{
			return null;
		}
		return m_cmDifficulty[(int)nType].NoFraise.getRandomElement();
	}
}
