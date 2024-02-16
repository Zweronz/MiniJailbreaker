using UnityEngine;

public class SkyCtrl : MonoBehaviour
{
	public Transform LightningList;

	private float m_fTime;

	private float m_fLastTime;

	private int m_nLast;

	private void Start()
	{
		m_fTime = Time.realtimeSinceStartup;
		m_fLastTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (globalVal.GamePause || null == LightningList || 1f >= Time.realtimeSinceStartup - m_fLastTime || !((float)Random.RandomRange(20, 40) < Time.realtimeSinceStartup - m_fTime))
		{
			return;
		}
		int num = Random.RandomRange(1, LightningList.childCount);
		if (m_nLast == num)
		{
			num++;
			num %= LightningList.childCount;
			num++;
		}
		Transform child = LightningList.GetChild(num - 1);
		if (null != child)
		{
			Lightning lightning = child.GetComponent(typeof(Lightning)) as Lightning;
			lightning.startPlay();
			m_nLast = num;
		}
		m_fTime = Time.realtimeSinceStartup;
		if (2f < Time.realtimeSinceStartup - m_fLastTime)
		{
			num = Random.RandomRange(1, 100);
			if (30 >= num)
			{
				m_fTime -= 30f;
			}
			else if (40 >= num)
			{
				m_fTime -= 20f;
			}
			else if (60 >= num)
			{
				m_fTime -= 10f;
			}
		}
		m_fLastTime = Time.realtimeSinceStartup;
	}
}
