using UnityEngine;

public class Plane : MonoBehaviour
{
	public enum ENUM_PLANE_STATUS
	{
		ENUM_PLANE_IDLE = 0,
		ENUM_PLANE_FLYCOM = 1,
		ENUM_PLANE_STAY = 2,
		ENUM_PLANE_FLYGO = 3
	}

	private const float c_fPlaneHeight = 20f;

	private const string c_strSound = "FX_helicopter_loop";

	private Transform m_cmSearchLight1;

	private Transform m_cmSearchLight2;

	private Transform m_cmPlayer;

	private ENUM_PLANE_STATUS m_nStatus;

	private bool m_bFly;

	private bool m_bStay;

	private float m_fTime;

	private float m_fDistance;

	public Transform SearchLight1
	{
		set
		{
			m_cmSearchLight1 = value;
		}
	}

	public Transform SearchLight2
	{
		set
		{
			m_cmSearchLight2 = value;
		}
	}

	public Transform Player
	{
		set
		{
			m_cmPlayer = value;
		}
	}

	private void Start()
	{
		m_nStatus = ENUM_PLANE_STATUS.ENUM_PLANE_IDLE;
	}

	private void Update()
	{
		if (m_nStatus != 0 && !(null == m_cmPlayer))
		{
			switch (m_nStatus)
			{
			case ENUM_PLANE_STATUS.ENUM_PLANE_FLYCOM:
				flyCom();
				break;
			case ENUM_PLANE_STATUS.ENUM_PLANE_STAY:
				stay();
				break;
			case ENUM_PLANE_STATUS.ENUM_PLANE_FLYGO:
				flyGo();
				break;
			}
			GameAudio.GetInstance().resetPosition("FX_helicopter_loop", base.transform.position);
		}
	}

	public void idle()
	{
	}

	public void flyCom()
	{
		m_fDistance -= Time.deltaTime * 100f;
		if (0f > m_fDistance)
		{
			m_fDistance = 0f;
		}
		Vector3 position = m_cmPlayer.position - m_cmPlayer.forward * m_fDistance;
		position.y += 20f;
		base.transform.position = position;
		if (m_fDistance == 0f)
		{
			m_nStatus = ENUM_PLANE_STATUS.ENUM_PLANE_STAY;
			OpenSearchLight();
			m_fTime += 2f;
		}
	}

	public void stay()
	{
		if (Time.realtimeSinceStartup >= m_fTime)
		{
			m_nStatus = ENUM_PLANE_STATUS.ENUM_PLANE_FLYGO;
			m_fDistance = 0f;
		}
		Vector3 position = m_cmPlayer.position;
		position.y += 20f;
		base.transform.position = position;
	}

	public void flyGo()
	{
		m_fDistance += Time.deltaTime * 100f;
		if (50f < m_fDistance)
		{
			m_fDistance = 50f;
		}
		Vector3 position = m_cmPlayer.position + m_cmPlayer.forward * m_fDistance;
		position.y += 20f;
		base.transform.position = position;
		if (m_fDistance == 50f)
		{
			m_nStatus = ENUM_PLANE_STATUS.ENUM_PLANE_IDLE;
			base.transform.position = new Vector3(0f, 0f, -1000f);
		}
	}

	public void StartFly(bool bStay)
	{
		if (m_nStatus == ENUM_PLANE_STATUS.ENUM_PLANE_IDLE)
		{
			m_bFly = true;
			m_bStay = bStay;
			m_fDistance = 50f;
			m_nStatus = ENUM_PLANE_STATUS.ENUM_PLANE_FLYCOM;
			OpenSound();
		}
	}

	public void OpenSearchLight()
	{
		m_fTime = (float)Random.RandomRange(3, 5) + Time.realtimeSinceStartup;
		if (null != m_cmSearchLight1)
		{
			(m_cmSearchLight1.GetComponent(typeof(Searchlight)) as Searchlight).StartParade(false, m_fTime);
		}
		if (null != m_cmSearchLight2)
		{
			(m_cmSearchLight2.GetComponent(typeof(Searchlight)) as Searchlight).StartParade(false, m_fTime);
		}
	}

	public void OpenSound()
	{
		GameAudio.GetInstance().playSound("FX_helicopter_loop", base.transform.position);
	}
}
