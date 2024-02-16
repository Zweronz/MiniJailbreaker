using UnityEngine;

public struct BufferInfo
{
	private ENUM_CHARA_BUFFER m_nBuffer;

	private float m_fBufferTime;

	private int m_nStrength;

	private string m_strEffect;

	private string m_strStartAudio;

	private string m_strEndAudio;

	private Transform m_cmEffect;

	public ENUM_CHARA_BUFFER Buffer
	{
		get
		{
			return m_nBuffer;
		}
		set
		{
			m_nBuffer = value;
		}
	}

	public float BufferTime
	{
		get
		{
			return m_fBufferTime;
		}
		set
		{
			m_fBufferTime = value;
		}
	}

	public int Strength
	{
		get
		{
			return m_nStrength;
		}
		set
		{
			m_nStrength = value;
		}
	}

	public string EffectName
	{
		get
		{
			return m_strEffect;
		}
		set
		{
			m_strEffect = value;
		}
	}

	public string StartAudio
	{
		get
		{
			return m_strStartAudio;
		}
		set
		{
			m_strStartAudio = value;
		}
	}

	public string EndAudio
	{
		get
		{
			return m_strEndAudio;
		}
		set
		{
			m_strEndAudio = value;
		}
	}

	public Transform Effect
	{
		get
		{
			return m_cmEffect;
		}
		set
		{
			m_cmEffect = value;
		}
	}

	public bool isTime()
	{
		if (m_fBufferTime != -1f && Time.realtimeSinceStartup >= m_fBufferTime)
		{
			return true;
		}
		return false;
	}
}
