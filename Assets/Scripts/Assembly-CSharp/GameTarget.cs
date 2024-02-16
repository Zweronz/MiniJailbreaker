using UnityEngine;

public class GameTarget
{
	private const float c_fAngle = 5f;

	private Transform m_cmPlayer;

	private Transform m_cmEnemy;

	public Transform Player
	{
		set
		{
			m_cmPlayer = value;
		}
	}

	public Transform Enemy
	{
		get
		{
			return m_cmEnemy;
		}
		set
		{
			if (isMinDistance(value) && checkAngle(value))
			{
				m_cmEnemy = value;
			}
		}
	}

	public bool isMinDistance(Transform cmEnemy)
	{
		if (null == m_cmEnemy)
		{
			return true;
		}
		GameEnemy gameEnemy = m_cmEnemy.GetComponent(typeof(GameEnemy)) as GameEnemy;
		GameEnemy gameEnemy2 = cmEnemy.GetComponent(typeof(GameEnemy)) as GameEnemy;
		float num = Vector3.Distance(m_cmPlayer.position, gameEnemy.EnemyInstance.position);
		float num2 = Vector3.Distance(m_cmPlayer.position, gameEnemy2.EnemyInstance.position);
		if (num >= num2)
		{
			return true;
		}
		return false;
	}

	public void Reset()
	{
		m_cmEnemy = null;
	}

	public void Dead(Transform cmEnemy)
	{
		if (cmEnemy.GetInstanceID() == m_cmEnemy.GetInstanceID())
		{
			m_cmEnemy = null;
		}
	}

	public bool checkAngle(Transform cmEnemy)
	{
		if (null == m_cmPlayer || null == cmEnemy)
		{
			return false;
		}
		GameEnemy gameEnemy = cmEnemy.GetComponent(typeof(GameEnemy)) as GameEnemy;
		Vector3 value = gameEnemy.EnemyInstance.position - m_cmPlayer.position;
		value.y = 0f;
		value = Vector3.Normalize(value);
		if (5f >= Vector3.Angle(value, m_cmPlayer.forward))
		{
			return true;
		}
		return false;
	}

	public void Run()
	{
		if (!(null == m_cmPlayer) && !(null == m_cmEnemy) && !checkAngle(m_cmEnemy))
		{
			m_cmEnemy = null;
		}
	}

	public void Destroy()
	{
		m_cmEnemy = null;
		m_cmPlayer = null;
	}
}
