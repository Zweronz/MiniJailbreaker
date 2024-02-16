using UnityEngine;

public class ShotTraget
{
	private Transform m_cmEffect;

	private Transform m_cmBullet;

	private float m_fSpeed;

	private Vector3 m_v3Target;

	private Transform m_cmGunpoint;

	private float m_fTime;

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

	public Transform Bullet
	{
		get
		{
			return m_cmBullet;
		}
		set
		{
			m_cmBullet = value;
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

	public Vector3 Target
	{
		get
		{
			return m_v3Target;
		}
		set
		{
			m_v3Target = value;
		}
	}

	public Transform Gunpoint
	{
		get
		{
			return m_cmGunpoint;
		}
		set
		{
			m_cmGunpoint = value;
		}
	}

	public float ShotTime
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
}
