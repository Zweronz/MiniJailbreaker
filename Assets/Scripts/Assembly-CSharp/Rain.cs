using UnityEngine;

public class Rain : MonoBehaviour
{
	private float m_fTime;

	private float m_fAlpha;

	private bool m_bPlaySound;

	private void Start()
	{
		Color color = base.transform.GetComponent<Renderer>().material.GetColor("_Color");
		color.a = 0f;
		base.transform.GetComponent<Renderer>().material.SetColor("_Color", color);
	}

	private void Update()
	{
		if (Time.realtimeSinceStartup >= m_fTime && 0f < m_fAlpha)
		{
			m_fAlpha -= Time.deltaTime;
			Color color = base.transform.GetComponent<Renderer>().material.GetColor("_Color");
			color.a = m_fAlpha;
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", color);
		}
		if (0f >= m_fAlpha && m_bPlaySound)
		{
			GameAudio.GetInstance().stopSound("Amb_Rain");
			m_bPlaySound = false;
		}
	}

	public void startRain()
	{
		if (!(0f < m_fAlpha))
		{
			m_fTime = Random.RandomRange(25, 45);
			m_fTime += Time.realtimeSinceStartup;
			m_fAlpha = 1f;
			Color color = base.transform.GetComponent<Renderer>().material.GetColor("_Color");
			color.a = m_fAlpha;
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", color);
			GameAudio.GetInstance().playSound("Amb_Rain", Vector3.zero);
			m_bPlaySound = true;
		}
	}
}
