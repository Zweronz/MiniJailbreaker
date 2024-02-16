using UnityEngine;

public class GameViewMovie : MonoBehaviour
{
	public Vector3[] WayPoint;

	public float[] Speed;

	private int m_nIndex;

	private void Start()
	{
		m_nIndex = -1;
	}

	private void Update()
	{
	}

	public void Reset()
	{
		m_nIndex = -1;
	}

	public Vector3 getNextPoint()
	{
		m_nIndex++;
		if (WayPoint.Length > m_nIndex)
		{
			return WayPoint[m_nIndex];
		}
		return Vector3.zero;
	}

	public float getCurSpeed()
	{
		if (Speed.Length > m_nIndex)
		{
			return Speed[m_nIndex];
		}
		return 0f;
	}
}
