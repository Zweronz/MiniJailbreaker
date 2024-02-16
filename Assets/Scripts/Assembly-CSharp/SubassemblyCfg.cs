public class SubassemblyCfg
{
	private string m_strCode;

	private int m_nMinSpace;

	private int m_nSafety;

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

	public int MinSpace
	{
		get
		{
			return m_nMinSpace;
		}
		set
		{
			m_nMinSpace = value;
		}
	}

	public int Safety
	{
		get
		{
			return m_nSafety;
		}
		set
		{
			m_nSafety = value;
		}
	}
}
