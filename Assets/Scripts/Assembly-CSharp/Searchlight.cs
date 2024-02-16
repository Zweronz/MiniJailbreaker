using UnityEngine;

public class Searchlight : MonoBehaviour
{
	private enum ENUM_SEARCHLIGHT
	{
		SEARCHLIGHT_UNSHOW = 0,
		SEARCHLIGHT_PARADE = 1,
		SEARCHLIGHT_FOCUS = 2,
		SEARCHLIGHT_STAY = 3,
		SEARCHLIGHT_DISAPPEAR = 4
	}

	private const float c_fMaxOffset = 0.48f;

	private const float c_fMoveDis = 5f;

	public bool Parade;

	private bool m_bParade;

	private int m_nForward;

	private int m_nRight;

	private float m_fParadeDisX;

	private bool m_bStay;

	private ENUM_SEARCHLIGHT m_nStatus;

	private float m_fParadeTime;

	private float m_fStayTime;

	private void Start()
	{
		m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_UNSHOW;
	}

	private void Update()
	{
		if (Parade)
		{
			StartParade();
			Parade = false;
		}
		switch (m_nStatus)
		{
		case ENUM_SEARCHLIGHT.SEARCHLIGHT_UNSHOW:
			if (base.transform.gameObject.activeInHierarchy)
			{
				base.transform.gameObject.SetActiveRecursively(false);
			}
			break;
		case ENUM_SEARCHLIGHT.SEARCHLIGHT_PARADE:
			ParadeX();
			ParadeY();
			if (Time.realtimeSinceStartup >= m_fParadeTime)
			{
				if (m_bStay)
				{
					m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_FOCUS;
					break;
				}
				m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_DISAPPEAR;
				m_fParadeDisX = 0f;
			}
			break;
		case ENUM_SEARCHLIGHT.SEARCHLIGHT_FOCUS:
			Focus();
			break;
		case ENUM_SEARCHLIGHT.SEARCHLIGHT_STAY:
			if (Time.realtimeSinceStartup >= m_fStayTime)
			{
				m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_DISAPPEAR;
				m_fParadeDisX = 0f;
			}
			break;
		case ENUM_SEARCHLIGHT.SEARCHLIGHT_DISAPPEAR:
			Disappear();
			break;
		}
	}

	public void ParadeX()
	{
		float x = base.transform.GetComponent<Renderer>().material.mainTextureOffset.x;
		if (0f >= (float)(-m_nRight) * m_fParadeDisX)
		{
			m_fParadeDisX = Random.RandomRange(1f, 48f) / 100f;
			m_fParadeDisX *= m_nRight;
			if (!m_bStay)
			{
				m_nRight *= -1;
			}
		}
		float num = Time.deltaTime * (float)(-m_nRight) * 0.3f;
		m_fParadeDisX -= num;
		x += num;
		if (-0.48f > x)
		{
			x = -0.48f;
		}
		else if (0.48f < x)
		{
			x = 0.48f;
		}
		base.transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, 0f);
	}

	public void ParadeY()
	{
		Vector3 localPosition = base.transform.localPosition;
		if (-5f >= localPosition.z)
		{
			m_nForward = 1;
		}
		else if (5f <= localPosition.z)
		{
			m_nForward = -1;
		}
		localPosition.z += Time.deltaTime * (float)m_nForward * 5f;
		base.transform.localPosition = localPosition;
	}

	private void Disappear()
	{
		float x = base.transform.GetComponent<Renderer>().material.mainTextureOffset.x;
		if (m_fParadeDisX == 0f)
		{
			if (0f > x)
			{
				m_fParadeDisX = 0.48f - x;
			}
			else
			{
				m_fParadeDisX = -0.52f - x;
			}
		}
		float num = Time.deltaTime * (float)(-m_nRight) * 0.3f;
		m_fParadeDisX -= num;
		x += num;
		base.transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(x, 0f);
		if (0f >= (float)(-m_nRight) * m_fParadeDisX)
		{
			m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_UNSHOW;
		}
	}

	private void Focus()
	{
		float num = base.transform.GetComponent<Renderer>().material.mainTextureOffset.x;
		Vector3 localPosition = base.transform.localPosition;
		if (0f > num)
		{
			num += Time.deltaTime * 0.3f;
		}
		else if (0f < num)
		{
			num -= Time.deltaTime * 0.3f;
		}
		base.transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(num, 0f);
		if (0f > localPosition.z)
		{
			localPosition.z += Time.deltaTime * 5f;
		}
		else if (0f < localPosition.y)
		{
			localPosition.z -= Time.deltaTime * 5f;
		}
		base.transform.localPosition = localPosition;
		if (num == 0f && localPosition.z == 0f)
		{
			m_fStayTime = Time.realtimeSinceStartup + 2f;
			m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_STAY;
		}
	}

	public void StartParade()
	{
		StartParade(false);
	}

	public void StartParade(bool bStay)
	{
		float fTime = (float)Random.RandomRange(3, 5) + Time.realtimeSinceStartup;
		StartParade(bStay, fTime);
	}

	public void StartParade(bool bStay, float fTime)
	{
		if (m_nStatus == ENUM_SEARCHLIGHT.SEARCHLIGHT_UNSHOW)
		{
			m_bParade = true;
			m_bStay = bStay;
			if (Random.RandomRange(1, 100) % 2 == 0)
			{
				m_nRight = 1;
			}
			else
			{
				m_nRight = -1;
			}
			base.transform.GetComponent<Renderer>().material.mainTextureOffset = new Vector2((float)m_nRight * 0.48f, 0f);
			m_fParadeDisX = 0f;
			m_nRight *= -1;
			m_nForward = -1;
			m_nStatus = ENUM_SEARCHLIGHT.SEARCHLIGHT_PARADE;
			m_fParadeTime = fTime;
			if (!base.transform.gameObject.activeInHierarchy)
			{
				base.transform.gameObject.SetActiveRecursively(true);
			}
		}
	}
}
