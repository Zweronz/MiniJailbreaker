using UnityEngine;

public class SpeedRecord
{
	private int m_nSpeed;

	private float m_fTime;

	private int m_nIndex;

	public int Speed
	{
		get
		{
			return m_nSpeed;
		}
		set
		{
			m_nSpeed = value;
		}
	}

	public float StopTime
	{
		get
		{
			return m_fTime;
		}
		set
		{
			m_fTime = value;
		}
	}

	public int Index
	{
		get
		{
			return m_nIndex;
		}
		set
		{
			m_nIndex = value;
		}
	}

	public bool isTime()
	{
		if (Time.realtimeSinceStartup >= m_fTime)
		{
			return true;
		}
		return false;
	}
}
