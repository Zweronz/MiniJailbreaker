public class ItemProbability
{
	private string m_strCode;

	private int[] m_nDifficulty;

	private float m_fRunway;

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

	public int this[ENUM_DIFFICULTY_TYPE nType]
	{
		get
		{
			if (m_nDifficulty == null)
			{
				return 0;
			}
			return m_nDifficulty[(int)nType];
		}
		set
		{
			if (m_nDifficulty == null)
			{
				m_nDifficulty = new int[8];
			}
			m_nDifficulty[(int)nType] = value;
		}
	}

	public float Runway
	{
		get
		{
			return m_fRunway;
		}
		set
		{
			m_fRunway = value;
		}
	}
}
