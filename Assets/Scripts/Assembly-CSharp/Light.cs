using UnityEngine;

public class Light : MonoBehaviour
{
	private enum LIGHT_STATUS
	{
		LIGHT_START_OPEN = 0,
		LIGHT_BRIGHTEN = 1,
		LIGHT_OPENING = 2,
		LIGHT_START_CLOSE = 3,
		LIGHT_DARKEN = 4,
		LIGHT_CLOSE = 5
	}

	public Transform cmLight;

	private float m_fAlpha;

	private float m_fTime;

	private LIGHT_STATUS m_nStatus;

	private void Start()
	{
		m_fTime = Time.realtimeSinceStartup;
		m_nStatus = LIGHT_STATUS.LIGHT_OPENING;
	}

	private void Update()
	{
		if (null == cmLight)
		{
			return;
		}
		switch (m_nStatus)
		{
		case LIGHT_STATUS.LIGHT_START_OPEN:
			Open();
			break;
		case LIGHT_STATUS.LIGHT_BRIGHTEN:
			Brighten();
			break;
		case LIGHT_STATUS.LIGHT_OPENING:
			if (Time.realtimeSinceStartup - m_fTime > (float)Random.RandomRange(10, 15))
			{
				m_nStatus = LIGHT_STATUS.LIGHT_START_CLOSE;
			}
			break;
		case LIGHT_STATUS.LIGHT_START_CLOSE:
			Close();
			break;
		case LIGHT_STATUS.LIGHT_DARKEN:
			Darken();
			break;
		case LIGHT_STATUS.LIGHT_CLOSE:
			if (Time.realtimeSinceStartup - m_fTime > (float)Random.RandomRange(5, 10))
			{
				m_nStatus = LIGHT_STATUS.LIGHT_START_OPEN;
			}
			break;
		}
	}

	private void Open()
	{
		int num = Random.RandomRange(1, 100);
		if (30 >= num)
		{
			m_nStatus = LIGHT_STATUS.LIGHT_BRIGHTEN;
		}
		else if (2 <= num % 4)
		{
			setAlpha(0f);
		}
		else
		{
			setAlpha(1f);
		}
	}

	private void Brighten()
	{
		m_fAlpha += Time.deltaTime * 1.5f;
		setAlpha(m_fAlpha);
		if (1f <= m_fAlpha)
		{
			m_nStatus = LIGHT_STATUS.LIGHT_OPENING;
			m_fTime = Time.realtimeSinceStartup;
		}
	}

	private void Close()
	{
		int num = Random.RandomRange(1, 100);
		if (30 >= num)
		{
			m_nStatus = LIGHT_STATUS.LIGHT_DARKEN;
		}
		else if (2 <= num % 4)
		{
			setAlpha(0f);
		}
		else
		{
			setAlpha(1f);
		}
	}

	private void Darken()
	{
		m_fAlpha -= Time.deltaTime * 2.5f;
		setAlpha(m_fAlpha);
		if (0f >= m_fAlpha)
		{
			m_nStatus = LIGHT_STATUS.LIGHT_CLOSE;
			m_fTime = Time.realtimeSinceStartup;
		}
	}

	private void setAlpha(float fAlpha)
	{
		Color color = cmLight.GetComponent<Renderer>().material.GetColor("_Color");
		color.a = fAlpha;
		cmLight.GetComponent<Renderer>().material.SetColor("_Color", color);
	}
}
