using UnityEngine;

public class SkyBox : MonoBehaviour
{
	private int m_nCurIndex;

	private int m_nNextIndex;

	private Material[] cmMaterials;

	private bool m_bStartChange;

	public float ChangeSpeed;

	private float m_fLastTime;

	private void Start()
	{
		m_bStartChange = false;
		cmMaterials = base.transform.GetComponent<Renderer>().materials;
		m_fLastTime = Time.realtimeSinceStartup;
		m_nCurIndex = 0;
	}

	private void Update()
	{
		if (!m_bStartChange || 0 >= cmMaterials.Length || null == cmMaterials[m_nNextIndex])
		{
			return;
		}
		if (Shader.Find("Sonke/AlphaCtrl") != cmMaterials[m_nNextIndex].shader)
		{
			m_bStartChange = false;
			return;
		}
		float a = cmMaterials[m_nNextIndex].GetColor("_Color").a;
		a += Time.deltaTime * ChangeSpeed;
		cmMaterials[m_nNextIndex].SetColor("_Color", new Color(1f, 1f, 1f, a));
		cmMaterials[m_nCurIndex].SetColor("_Color", new Color(1f, 1f, 1f, 1f - a));
		if (a >= 1f)
		{
			m_bStartChange = false;
			m_nCurIndex = m_nNextIndex;
		}
	}

	public void ChangeSky()
	{
		if (0 < cmMaterials.Length)
		{
			int num = Random.RandomRange(1, cmMaterials.Length * 100);
			m_nNextIndex = num % cmMaterials.Length;
			if (m_nCurIndex == m_nNextIndex)
			{
				m_nNextIndex++;
				m_nNextIndex %= cmMaterials.Length;
			}
			m_bStartChange = true;
		}
	}
}
