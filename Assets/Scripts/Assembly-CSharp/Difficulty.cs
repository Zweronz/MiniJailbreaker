using System;

[Serializable]
public class Difficulty
{
	public ENUM_DIFFICULTY_TYPE m_nType;

	public ProbabilityList m_cmNextChannel;

	public ProbabilityList m_cmFraise;

	public ProbabilityList m_cmNoFraise;

	public ENUM_DIFFICULTY_TYPE Type
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

	public ProbabilityList NextChannel
	{
		get
		{
			if (m_cmNextChannel == null)
			{
				m_cmNextChannel = new ProbabilityList();
			}
			return m_cmNextChannel;
		}
	}

	public ProbabilityList Fraise
	{
		get
		{
			if (m_cmFraise == null)
			{
				m_cmFraise = new ProbabilityList();
			}
			return m_cmFraise;
		}
	}

	public ProbabilityList NoFraise
	{
		get
		{
			if (m_cmNoFraise == null)
			{
				m_cmNoFraise = new ProbabilityList();
			}
			return m_cmNoFraise;
		}
	}
}
