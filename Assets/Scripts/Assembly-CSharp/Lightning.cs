using UnityEngine;

public class Lightning : MonoBehaviour
{
	private float m_fScaleX;

	private float m_fScaleY;

	private float m_fAlpha;

	private int m_nScaleCount;

	public bool StartPlay;

	private Transform m_cmLightning;

	private float m_fLastTime;

	private bool m_bPlaySound;

	private void Start()
	{
		m_fScaleX = 0f;
		m_fScaleY = 0f;
		base.transform.localScale = new Vector3(m_fScaleX, m_fScaleY, 1f);
		if (null == m_cmLightning)
		{
			GameObject gameObject = GameObject.Find("TUI/TUIControl");
			m_cmLightning = gameObject.transform.Find("Lightning");
		}
		m_fLastTime = Time.realtimeSinceStartup;
	}

	private void Update()
	{
		if (StartPlay)
		{
			startPlay();
			StartPlay = false;
		}
		if (0f >= m_fAlpha)
		{
			return;
		}
		if (7 >= m_nScaleCount)
		{
			float num = (float)Random.RandomRange(2, 5) / 10f;
			m_fScaleX += num;
			m_fScaleY += num * 4f;
			base.transform.localScale = new Vector3(m_fScaleX, m_fScaleY, 1f);
			m_nScaleCount++;
			return;
		}
		m_fAlpha -= (Time.realtimeSinceStartup - m_fLastTime) * 1.5f;
		m_fLastTime = Time.realtimeSinceStartup;
		Color color = base.transform.GetComponent<Renderer>().material.GetColor("_Color");
		color.a = m_fAlpha;
		base.transform.GetComponent<Renderer>().material.SetColor("_Color", color);
		if (0f >= m_fAlpha && 50 <= Random.RandomRange(1, 100))
		{
		}
	}

	public void startPlay()
	{
		m_fAlpha = 1f;
		m_fScaleX = 0f;
		m_fScaleY = 0f;
		m_nScaleCount = 0;
		showLightning();
		GameAudio.GetInstance().playSound("Amb_thunder", Vector3.zero);
		m_bPlaySound = true;
	}

	public void showLightning()
	{
		if (null != m_cmLightning)
		{
			LightningScreen lightningScreen = m_cmLightning.GetComponent(typeof(LightningScreen)) as LightningScreen;
			if (null != lightningScreen)
			{
				lightningScreen.StartGlitter();
			}
		}
	}

	public void showRain()
	{
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		Transform transform = null;
		int num = Random.RandomRange(1, 100);
		for (int i = 1; i < 4; i++)
		{
			transform = gameObject.transform.Find("Rain" + i);
			if (null != transform)
			{
				Rain rain = transform.GetComponent(typeof(Rain)) as Rain;
				rain.startRain();
			}
		}
	}
}
