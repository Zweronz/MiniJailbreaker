public class MonsterCfg : SubassemblyCfg
{
	private string m_strInstance;

	private float m_fMoveRange;

	private float m_fMoveSpeed;

	private float m_fAttackRange;

	private float m_fAttackSpeed;

	private int m_nHP;

	private int m_nEnergy;

	public string Instance
	{
		get
		{
			return m_strInstance;
		}
		set
		{
			m_strInstance = value;
		}
	}

	public float MoveRange
	{
		get
		{
			return m_fMoveRange;
		}
		set
		{
			m_fMoveRange = value;
		}
	}

	public float MoveSpeed
	{
		get
		{
			return m_fMoveSpeed;
		}
		set
		{
			m_fMoveSpeed = value;
		}
	}

	public float AttackRange
	{
		get
		{
			return m_fAttackRange;
		}
		set
		{
			m_fAttackRange = value;
		}
	}

	public float AttackSpeed
	{
		get
		{
			return m_fAttackSpeed;
		}
		set
		{
			m_fAttackSpeed = value;
		}
	}

	public int HP
	{
		get
		{
			return m_nHP;
		}
		set
		{
			m_nHP = value;
		}
	}

	public int Energy
	{
		get
		{
			return m_nEnergy;
		}
		set
		{
			m_nEnergy = value;
		}
	}
}
