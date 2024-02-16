using UnityEngine;

public class FrameEffect : MonoBehaviour
{
	public string[] m_strBloodEffect;

	private int m_nFrameCount;

	private bool m_bShow;

	private TUIMeshSprite m_cmSprite;

	public bool isClose
	{
		get
		{
			return m_bShow;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bShow)
		{
			return;
		}
		if (null == m_cmSprite)
		{
			m_cmSprite = base.transform.GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
		}
		if (!(null == m_cmSprite) && m_strBloodEffect != null && 0 < m_strBloodEffect.Length)
		{
			if (m_nFrameCount >= m_strBloodEffect.Length)
			{
				m_bShow = false;
				return;
			}
			m_cmSprite.frameName = m_strBloodEffect[m_nFrameCount];
			m_cmSprite.UpdateMesh();
			m_nFrameCount++;
		}
	}

	public void showEffect()
	{
		m_bShow = true;
		m_nFrameCount = 0;
	}
}
