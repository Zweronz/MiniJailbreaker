using UnityEngine;

public class GameItemHide : MonoBehaviour
{
	private bool m_bHide;

	private float m_fHideTime;

	private void Start()
	{
		m_bHide = false;
	}

	private void Update()
	{
		if (m_bHide && Time.realtimeSinceStartup >= m_fHideTime)
		{
			Hide(false);
		}
	}

	public void Hide(bool bHide)
	{
		if (bHide == m_bHide)
		{
			return;
		}
		float a = 1f;
		if (bHide)
		{
			a = 0.4f;
		}
		Material[] materials = base.transform.GetComponent<Renderer>().materials;
		foreach (Material material in materials)
		{
			if (Shader.Find("Sonke/AlphaCtrl") == material.shader)
			{
				material.SetColor("_Color", new Color(1f, 1f, 1f, a));
			}
		}
		m_bHide = bHide;
		m_fHideTime = Time.realtimeSinceStartup;
	}
}
