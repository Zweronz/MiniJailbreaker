using UnityEngine;

public struct FixNode
{
	private string m_strCode;

	private TerrainNode m_cmNode;

	private float m_fRunWay;

	private Vector3 m_v3Position;

	private Vector3 m_v3Forward;

	private Vector3 m_v3EulerAngles;

	private bool m_bIsMonster;

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

	public TerrainNode Node
	{
		get
		{
			return m_cmNode;
		}
		set
		{
			m_cmNode = value;
		}
	}

	public float RunWay
	{
		get
		{
			return m_fRunWay;
		}
		set
		{
			m_fRunWay = value;
		}
	}

	public Vector3 Position
	{
		get
		{
			return m_v3Position;
		}
		set
		{
			m_v3Position = value;
		}
	}

	public Vector3 Forward
	{
		get
		{
			return m_v3Forward;
		}
		set
		{
			m_v3Forward = value;
		}
	}

	public Vector3 EulerAngles
	{
		get
		{
			return m_v3EulerAngles;
		}
		set
		{
			m_v3EulerAngles = value;
		}
	}

	public bool IsMonster
	{
		get
		{
			return m_bIsMonster;
		}
		set
		{
			m_bIsMonster = value;
		}
	}
}
