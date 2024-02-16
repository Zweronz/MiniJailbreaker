using UnityEngine;

public class LightningScreen : MonoBehaviour
{
	private Material m_cmMaterial;

	private float m_fGlitterAlpha;

	private void Start()
	{
		m_fGlitterAlpha = 0f;
		if (null == m_cmMaterial)
		{
			m_cmMaterial = base.transform.GetComponent<Renderer>().material;
		}
	}

	private void Update()
	{
		if (globalVal.GamePause)
		{
			return;
		}
		if (null == m_cmMaterial || 0f >= m_fGlitterAlpha)
		{
			base.transform.gameObject.SetActiveRecursively(false);
			return;
		}
		m_fGlitterAlpha -= Time.deltaTime;
		if (0f > m_fGlitterAlpha)
		{
			m_fGlitterAlpha = 0f;
		}
		Color color = m_cmMaterial.GetColor("_Color");
		color.a = m_fGlitterAlpha;
		m_cmMaterial.SetColor("_Color", color);
	}

	public void StartGlitter()
	{
		if (!(null == m_cmMaterial))
		{
			m_fGlitterAlpha = 1f;
			base.transform.gameObject.SetActiveRecursively(true);
		}
	}
}
