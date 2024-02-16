using System.Collections;
using UnityEngine;

public class GameEffect : GamePool
{
	private struct Alarm
	{
		private Transform m_cmEffect;

		private float m_fTime;

		public Transform Effect
		{
			get
			{
				return m_cmEffect;
			}
		}

		public void setTime(Transform cmEffect, float fTime)
		{
			if (!(null == cmEffect))
			{
				m_cmEffect = cmEffect;
				m_fTime = Time.realtimeSinceStartup + fTime;
			}
		}

		public bool isTime()
		{
			return m_fTime <= Time.realtimeSinceStartup;
		}
	}

	private struct MoveEffect
	{
		private Transform m_cmEffect;

		private float m_fSpeed;

		private Vector3 m_v3End;

		public Transform Effect
		{
			get
			{
				return m_cmEffect;
			}
		}

		public void setTarget(Transform cmEffect, float fSpeed, Vector3 v3End)
		{
			if (!(null == cmEffect))
			{
				m_cmEffect = cmEffect;
				m_fSpeed = fSpeed;
				m_v3End = v3End;
			}
		}

		public bool doMove()
		{
			float num = Vector3.Distance(m_cmEffect.position, m_v3End);
			if (0.1f >= num)
			{
				return true;
			}
			if (m_fSpeed * Time.deltaTime <= num)
			{
				m_cmEffect.position += m_cmEffect.forward * (m_fSpeed * Time.deltaTime);
			}
			else
			{
				m_cmEffect.position += m_cmEffect.forward * num;
			}
			return false;
		}
	}

	private const string c_strInstancePath = "Prefabs/Effect/";

	private const string c_strPoolName = "Effect";

	private static GameEffect m_cmInstance;

	public ArrayList m_alAlarm;

	private ArrayList m_alMove;

	private float m_fTime;

	private GameEffect()
	{
	}

	public static GameEffect GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameEffect();
			m_cmInstance.Initialize("Prefabs/Effect/", "Effect");
		}
		return m_cmInstance;
	}

	public new void Destroy()
	{
		if (m_cmInstance != null)
		{
			m_cmInstance = null;
		}
		base.Destroy();
	}

	public Transform playEffect(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		Transform cmEffect = citeObject(strCode);
		return playEffect(cmEffect);
	}

	public Transform playEffect(Transform cmEffect)
	{
		if (null == cmEffect)
		{
			return null;
		}
		cmEffect.gameObject.SetActiveRecursively(true);
		ParticleSystem particleSystem = null;
		for (int i = 0; i < cmEffect.childCount; i++)
		{
			particleSystem = cmEffect.GetChild(i).GetComponent(typeof(ParticleSystem)) as ParticleSystem;
			if (null != particleSystem)
			{
				particleSystem.Play();
			}
		}
		return cmEffect;
	}

	public void showEffect(string strCode, Vector3 v3Position)
	{
		if (strCode != null && 0 < strCode.Length)
		{
			Transform cmEffect = citeObject(strCode);
			showEffect(cmEffect, v3Position);
		}
	}

	public void showEffect(string strCode, Vector3 v3Position, Vector3 v3Forward)
	{
		if (strCode != null && 0 < strCode.Length)
		{
			Transform cmEffect = citeObject(strCode);
			showEffect(cmEffect, v3Position, v3Forward);
		}
	}

	public void showEffect(Transform cmEffect, Vector3 v3Position)
	{
		if (!(null == cmEffect))
		{
			showEffect(cmEffect, v3Position, Vector3.forward);
		}
	}

	public void showEffect(Transform cmEffect, Vector3 v3Position, Vector3 v3Forward)
	{
		if (!(null == cmEffect))
		{
			cmEffect.position = v3Position;
			cmEffect.forward = v3Forward;
		}
	}

	public Transform getEffect(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		Transform transform = getObjectInstance(strCode);
		if (null == transform)
		{
			transform = createObjectInstance(strCode);
		}
		transform.gameObject.SetActiveRecursively(true);
		return transform;
	}

	public Transform getEffect(string strCode, Vector3 v3Position)
	{
		Transform effect = getEffect(strCode);
		if (null == effect)
		{
			return null;
		}
		effect.position = v3Position;
		return effect;
	}

	public Transform getEffect(string strCode, Vector3 v3Position, Vector3 v3Forward)
	{
		Transform effect = getEffect(strCode, v3Position);
		if (null == effect)
		{
			return null;
		}
		effect.forward = v3Forward;
		return effect;
	}

	public void closeEffect(string strCode)
	{
		if (strCode != null && 0 < strCode.Length)
		{
			Transform transform = citeObject(strCode);
			if (!(null == transform))
			{
				closeEffect(transform);
			}
		}
	}

	public void closeEffect(Transform cmEffect)
	{
		if (null == cmEffect)
		{
			return;
		}
		ParticleSystem particleSystem = null;
		for (int i = 0; i < cmEffect.childCount; i++)
		{
			particleSystem = cmEffect.GetChild(i).GetComponent(typeof(ParticleSystem)) as ParticleSystem;
			if (null != particleSystem)
			{
				particleSystem.Stop();
			}
		}
		cmEffect.gameObject.SetActiveRecursively(false);
	}

	public void recycleEffect(Transform cmEffect)
	{
		if (!(null == cmEffect))
		{
			cmEffect.gameObject.SetActiveRecursively(false);
			recycleObject(cmEffect);
		}
	}

	public void setAlarm(Transform cmEffect, float fTime)
	{
		Alarm alarm = default(Alarm);
		alarm.setTime(cmEffect, fTime);
		if (m_alAlarm == null)
		{
			m_alAlarm = new ArrayList();
		}
		m_alAlarm.Add(alarm);
	}

	public void setMove(Transform cmEffect, float fSpeed, Vector3 v3End)
	{
		MoveEffect moveEffect = default(MoveEffect);
		moveEffect.setTarget(cmEffect, fSpeed, v3End);
		if (m_alMove == null)
		{
			m_alMove = new ArrayList();
		}
		m_alMove.Add(moveEffect);
	}

	public void Run()
	{
		if (m_alAlarm != null)
		{
			for (int i = 0; i < m_alAlarm.Count; i++)
			{
				Alarm alarm = (Alarm)m_alAlarm[i];
				if (alarm.isTime())
				{
					m_alAlarm.RemoveAt(i);
					recycleEffect(alarm.Effect);
					return;
				}
			}
		}
		if (m_alMove == null)
		{
			return;
		}
		for (int j = 0; j < m_alMove.Count; j++)
		{
			MoveEffect moveEffect = (MoveEffect)m_alMove[j];
			if (moveEffect.doMove())
			{
				m_alMove.RemoveAt(j);
				recycleEffect(moveEffect.Effect);
				break;
			}
		}
	}
}
