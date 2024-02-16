public struct CharaInfo
{
	private int m_nCharaID;

	private string m_strName;

	private float m_fLift;

	private float m_fGravity;

	private float m_fSpeed;

	private string m_strMoveEffect;

	private string m_strJumpEffect;

	private string m_strColliderEffect;

	private string m_strFireEffect;

	private string m_strRun;

	private string m_strJump1;

	private string m_strJump2;

	private string m_strJumpIdle;

	private string m_strDown;

	private string m_strLeft;

	private string m_strRight;

	public int ID
	{
		get
		{
			return m_nCharaID;
		}
		set
		{
			m_nCharaID = value;
		}
	}

	public string Name
	{
		get
		{
			return m_strName;
		}
		set
		{
			m_strName = value;
		}
	}

	public float Lift
	{
		get
		{
			return m_fLift;
		}
		set
		{
			m_fLift = value;
		}
	}

	public float Gravity
	{
		get
		{
			return m_fGravity;
		}
		set
		{
			m_fGravity = value;
		}
	}

	public float Speed
	{
		get
		{
			return m_fSpeed;
		}
		set
		{
			m_fSpeed = value;
		}
	}

	public string MoveEffect
	{
		get
		{
			return m_strMoveEffect;
		}
		set
		{
			m_strMoveEffect = value;
		}
	}

	public string JumpEffect
	{
		get
		{
			return m_strJumpEffect;
		}
		set
		{
			m_strJumpEffect = value;
		}
	}

	public string ColliderEffect
	{
		get
		{
			return m_strColliderEffect;
		}
		set
		{
			m_strColliderEffect = value;
		}
	}

	public string FireEffect
	{
		get
		{
			return m_strFireEffect;
		}
		set
		{
			m_strFireEffect = value;
		}
	}

	public string Run
	{
		get
		{
			return m_strRun;
		}
		set
		{
			m_strRun = value;
		}
	}

	public string Jump1
	{
		get
		{
			return m_strJump1;
		}
		set
		{
			m_strJump1 = value;
		}
	}

	public string Jump2
	{
		get
		{
			return m_strJump2;
		}
		set
		{
			m_strJump2 = value;
		}
	}

	public string JumpIdle
	{
		get
		{
			return m_strJumpIdle;
		}
		set
		{
			m_strJumpIdle = value;
		}
	}

	public string Down
	{
		get
		{
			return m_strDown;
		}
		set
		{
			m_strDown = value;
		}
	}

	public string Left
	{
		get
		{
			return m_strLeft;
		}
		set
		{
			m_strLeft = value;
		}
	}

	public string Right
	{
		get
		{
			return m_strRight;
		}
		set
		{
			m_strRight = value;
		}
	}
}
