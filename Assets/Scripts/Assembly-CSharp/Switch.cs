using UnityEngine;

public class Switch : MonoBehaviour
{
	public enum SWITCH_STATUS
	{
		SWITCH_ON = 0,
		SWITCH_OFF = 1
	}

	public float Speed;

	public SWITCH_STATUS OnOff;

	private bool m_bDoSwitch;

	private string m_strNextLevel;

	private void Start()
	{
		Color color = base.transform.GetComponent<Renderer>().material.GetColor("_Color");
		if (OnOff == SWITCH_STATUS.SWITCH_ON)
		{
			color.a = 0f;
		}
		else if (OnOff == SWITCH_STATUS.SWITCH_OFF)
		{
			color.a = 1f;
		}
		base.transform.GetComponent<Renderer>().material.SetColor("_Color", color);
		base.transform.localScale = new Vector3(Screen.width, 1f, Screen.height);
	}

	private void Update()
	{
		if (m_bDoSwitch)
		{
			Color color = base.transform.GetComponent<Renderer>().material.GetColor("_Color");
			if (OnOff == SWITCH_STATUS.SWITCH_OFF)
			{
				color.a += Speed * Time.deltaTime;
			}
			else if (OnOff == SWITCH_STATUS.SWITCH_ON)
			{
				color.a -= Speed * Time.deltaTime;
			}
			base.transform.GetComponent<Renderer>().material.SetColor("_Color", color);
			if (OnOff == SWITCH_STATUS.SWITCH_OFF && 1f <= color.a)
			{
				Application.LoadLevel(m_strNextLevel);
				m_bDoSwitch = false;
			}
			else if (OnOff == SWITCH_STATUS.SWITCH_ON && 0f >= color.a)
			{
				m_bDoSwitch = false;
				base.transform.gameObject.SetActiveRecursively(false);
			}
		}
	}

	public void Open()
	{
		if (!m_bDoSwitch)
		{
			OnOff = SWITCH_STATUS.SWITCH_ON;
			m_bDoSwitch = true;
			base.transform.gameObject.SetActiveRecursively(true);
		}
	}

	public void Close(string strNextLevel)
	{
		if (!m_bDoSwitch)
		{
			OnOff = SWITCH_STATUS.SWITCH_OFF;
			m_bDoSwitch = true;
			m_strNextLevel = strNextLevel;
			base.transform.gameObject.SetActiveRecursively(true);
		}
	}
}
