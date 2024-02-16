using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AreaCfg
{
	public ENUM_AREA_TYPE m_nType;

	public List<string> m_alConvert;

	public int m_nMinDistance;

	public int m_nMaxDistance;

	public string m_strCemetery;

	public bool m_bShow;

	public ENUM_AREA_TYPE Type
	{
		get
		{
			return m_nType;
		}
		set
		{
			m_nType = value;
		}
	}

	public int MinDistance
	{
		get
		{
			return m_nMinDistance;
		}
		set
		{
			m_nMinDistance = value;
		}
	}

	public int MaxDistance
	{
		get
		{
			return m_nMaxDistance;
		}
		set
		{
			m_nMaxDistance = value;
		}
	}

	public string Cemetery
	{
		get
		{
			return m_strCemetery;
		}
		set
		{
			m_strCemetery = value;
		}
	}

	public bool ShowSky
	{
		get
		{
			return m_bShow;
		}
		set
		{
			m_bShow = value;
		}
	}

	public void addConvert(string strCode)
	{
		if (m_alConvert == null)
		{
			m_alConvert = new List<string>();
		}
		m_alConvert.Add(strCode);
	}

	public string getConvert(int nCurDistance)
	{
		if (0 >= m_alConvert.Count)
		{
			return null;
		}
		if (nCurDistance < m_nMinDistance)
		{
			return null;
		}
		float num = (float)(m_nMaxDistance - m_nMinDistance) / 100f;
		num *= (float)(nCurDistance - m_nMinDistance);
		int num2 = UnityEngine.Random.RandomRange(1, 10000) / 100;
		if (num2 > Mathf.FloorToInt(num))
		{
			return null;
		}
		num2 = UnityEngine.Random.RandomRange(1, 100);
		if (m_alConvert != null)
		{
			num2 %= m_alConvert.Count;
			return m_alConvert[num2];
		}
		return null;
	}
}
