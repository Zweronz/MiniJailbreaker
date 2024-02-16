using UnityEngine;

public class TerrainPingPang : TerrainTrigger
{
	public Transform Target;

	public PingPangPoint[] Points;

	public bool Triggered;

	private int m_nIndex;

	private float m_fStopTime;

	private void Start()
	{
	}

	private void Update()
	{
		if (null == Target || Points == null || 0 >= Points.Length || !Triggered)
		{
			return;
		}
		bool flag = false;
		if (!(Time.realtimeSinceStartup <= m_fStopTime + Points[m_nIndex].StopTime) && 0f < Points[m_nIndex].Speed)
		{
			float num = Vector3.Distance(Target.localPosition, Points[m_nIndex].Point);
			float num2 = Points[m_nIndex].Speed * Time.deltaTime;
			if (num2 >= num)
			{
				Target.localPosition = Points[m_nIndex].Point;
				flag = true;
			}
			else
			{
				Target.localPosition += num2 * Vector3.Normalize(Points[m_nIndex].Point - Target.localPosition);
			}
		}
		if (flag)
		{
			m_nIndex++;
			m_nIndex %= Points.Length;
			m_fStopTime = Time.realtimeSinceStartup;
		}
	}

	public new void Init()
	{
		m_nIndex = 0;
		if (!(null == Target) && Points != null && 0 < Points.Length)
		{
			Target.position = Points[m_nIndex].Point;
			m_nIndex++;
		}
	}

	public new void Trigger(Vector3 v3Point)
	{
		Triggered = true;
		m_fStopTime = Time.realtimeSinceStartup;
		Init();
	}
}
