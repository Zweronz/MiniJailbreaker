using System.Collections;
using UnityEngine;

public class TreasureCfg
{
	private Hashtable[] m_htDifficulty;

	public void addProbability(string strCode, int nLv1, int nLv2, int nLv3, int nLv4, int nLv5, int nLv6, int nLv7, int nLv8, string strEffect)
	{
		if (m_htDifficulty == null)
		{
			m_htDifficulty = new Hashtable[9];
			for (int i = 0; i < m_htDifficulty.Length; i++)
			{
				m_htDifficulty[i] = new Hashtable();
			}
		}
		m_htDifficulty[0].Add(strCode, nLv1);
		m_htDifficulty[1].Add(strCode, nLv2);
		m_htDifficulty[2].Add(strCode, nLv3);
		m_htDifficulty[3].Add(strCode, nLv4);
		m_htDifficulty[4].Add(strCode, nLv5);
		m_htDifficulty[5].Add(strCode, nLv6);
		m_htDifficulty[6].Add(strCode, nLv7);
		m_htDifficulty[7].Add(strCode, nLv8);
		m_htDifficulty[8].Add(strCode, strEffect);
	}

	public string getItem(ENUM_DIFFICULTY_TYPE nType)
	{
		int num = Random.Range(1, 10000);
		num /= 100;
		foreach (string key in m_htDifficulty[(int)nType].Keys)
		{
			if ((int)m_htDifficulty[(int)nType][key] >= num)
			{
				return key;
			}
			num -= (int)m_htDifficulty[(int)nType][key];
		}
		return null;
	}

	public string getEffect(string strCode)
	{
		return (string)m_htDifficulty[8][strCode];
	}
}
