using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProbabilityList
{
	public List<string> m_cmCodeList;

	public List<int> m_cmProbabilityList;

	public void addElement(string strCode, int nProbability)
	{
		if (m_cmCodeList == null)
		{
			m_cmCodeList = new List<string>();
		}
		if (m_cmProbabilityList == null)
		{
			m_cmProbabilityList = new List<int>();
		}
		m_cmCodeList.Add(strCode);
		m_cmProbabilityList.Add(nProbability);
	}

	public string getRandomElement()
	{
		if (m_cmCodeList == null || m_cmProbabilityList == null)
		{
			return null;
		}
		int num = UnityEngine.Random.RandomRange(1, 10000);
		num /= 100;
		for (int i = 0; i < m_cmCodeList.Count; i++)
		{
			if (m_cmProbabilityList[i] >= num)
			{
				return m_cmCodeList[i];
			}
			num -= m_cmProbabilityList[i];
		}
		return null;
	}
}
