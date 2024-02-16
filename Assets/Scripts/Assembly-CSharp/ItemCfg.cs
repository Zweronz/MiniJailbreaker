public class ItemCfg
{
	private string m_strCode;

	private ENUM_ITEM_TYPE m_nType;

	private float m_fTime;

	private string m_strEffect;

	private ENUM_CHARA_BUFFER[] m_alBufferType;

	private int[] m_alStength;

	private string[] m_alEffect;

	private string[] m_alStartAudio;

	private string[] m_alEndAudio;

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

	public ENUM_ITEM_TYPE ItemType
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

	public float Time
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

	public string OnceEffect
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

	public int BufferCount
	{
		get
		{
			return m_alBufferType.Length;
		}
		set
		{
			m_alBufferType = new ENUM_CHARA_BUFFER[value];
			m_alStength = new int[value];
			m_alEffect = new string[value];
			m_alStartAudio = new string[value];
			m_alEndAudio = new string[value];
		}
	}

	public bool setBufferType(int nIndex, ENUM_CHARA_BUFFER nValue)
	{
		if (m_alBufferType == null || m_alBufferType.Length <= nIndex)
		{
			return false;
		}
		m_alBufferType[nIndex] = nValue;
		return true;
	}

	public bool setBufferStength(int nIndex, int nValue)
	{
		if (m_alStength == null || m_alStength.Length <= nIndex)
		{
			return false;
		}
		m_alStength[nIndex] = nValue;
		return true;
	}

	public bool setBufferEffect(int nIndex, string strValue)
	{
		if (m_alEffect == null || m_alEffect.Length <= nIndex)
		{
			return false;
		}
		m_alEffect[nIndex] = strValue;
		return true;
	}

	public bool setBufferStartAudio(int nIndex, string strValue)
	{
		if (m_alStartAudio == null || m_alStartAudio.Length <= nIndex)
		{
			return false;
		}
		m_alStartAudio[nIndex] = strValue;
		return true;
	}

	public bool setBufferEndAudio(int nIndex, string strValue)
	{
		if (m_alEndAudio == null || m_alEndAudio.Length <= nIndex)
		{
			return false;
		}
		m_alEndAudio[nIndex] = strValue;
		return true;
	}

	public ENUM_CHARA_BUFFER getBufferType(int nIndex)
	{
		if (m_alBufferType == null || m_alBufferType.Length <= nIndex)
		{
			return ENUM_CHARA_BUFFER.BUFFER_NONE;
		}
		return m_alBufferType[nIndex];
	}

	public int getBufferStength(int nIndex)
	{
		if (m_alStength == null || m_alStength.Length <= nIndex)
		{
			return -1;
		}
		return m_alStength[nIndex];
	}

	public string getBufferEffect(int nIndex)
	{
		if (m_alEffect == null || m_alEffect.Length <= nIndex)
		{
			return null;
		}
		return m_alEffect[nIndex];
	}

	public string getBufferStartAudio(int nIndex)
	{
		if (m_alStartAudio == null || m_alStartAudio.Length <= nIndex)
		{
			return null;
		}
		return m_alStartAudio[nIndex];
	}

	public string getBufferEndAudio(int nIndex)
	{
		if (m_alEndAudio == null || m_alEndAudio.Length <= nIndex)
		{
			return null;
		}
		return m_alEndAudio[nIndex];
	}
}
