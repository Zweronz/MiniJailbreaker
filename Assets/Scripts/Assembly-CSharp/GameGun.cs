using System.Collections;
using UnityEngine;

public class GameGun : MonoBehaviour
{
	public delegate void OnAlarmEvent(ref int nIndex);

	private const float c_fShowTime = 0.5f;

	private const float c_fDistance = 1f;

	private OnAlarmEvent onAlamevent;

	public Transform[] Gunpoint;

	public ShotTraget[] ShotInfo;

	public float BulletSpeed;

	public string ShotEffect;

	private ArrayList m_alShot;

	public void SetAlarmEventDelegate(OnAlarmEvent onAlarmEventDelegate)
	{
		onAlamevent = onAlarmEventDelegate;
	}

	private void Start()
	{
		Transform transform = null;
		Transform transform2 = null;
		Transform transform3 = null;
	}

	private void shotEffect(ShotTraget cmTarget, bool bShow)
	{
		if (null == cmTarget.Effect)
		{
			return;
		}
		ParticleEmitter particleEmitter = null;
		for (int i = 0; i < cmTarget.Effect.childCount; i++)
		{
			particleEmitter = cmTarget.Effect.GetChild(i).GetComponent(typeof(ParticleEmitter)) as ParticleEmitter;
			if (null != particleEmitter)
			{
				particleEmitter.emit = bShow;
			}
			particleEmitter = null;
		}
		if (!bShow)
		{
			if (null != cmTarget.Effect)
			{
				globalVal.Effect.recycleEffect(cmTarget.Effect);
			}
			cmTarget.Effect = null;
		}
	}

	private bool doMove(ShotTraget cmTarget)
	{
		if (null == cmTarget.Bullet)
		{
			return true;
		}
		float num = Vector3.Distance(cmTarget.Target, cmTarget.Bullet.position);
		if (1f >= num)
		{
			globalVal.Effect.recycleEffect(cmTarget.Bullet);
			cmTarget.Bullet = null;
			return true;
		}
		if (cmTarget.Speed * Time.deltaTime <= num)
		{
			cmTarget.Bullet.position += cmTarget.Bullet.forward * (cmTarget.Speed * Time.deltaTime);
		}
		else
		{
			cmTarget.Bullet.position += cmTarget.Bullet.forward * num;
		}
		return false;
	}

	private bool doShot(ShotTraget cmTarget)
	{
		bool result = true;
		if (Time.realtimeSinceStartup >= cmTarget.ShotTime + 0.5f)
		{
			shotEffect(cmTarget, false);
		}
		else
		{
			result = false;
		}
		if (null != cmTarget.Effect)
		{
			cmTarget.Effect.position = cmTarget.Gunpoint.position;
		}
		if (!doMove(cmTarget))
		{
			result = false;
		}
		return result;
	}

	private void Update()
	{
		if (m_alShot == null || 0 >= m_alShot.Count)
		{
			return;
		}
		int count = m_alShot.Count;
		for (int i = 0; i < count; i++)
		{
			if (doShot((ShotTraget)m_alShot[i]))
			{
				m_alShot.RemoveAt(i);
				i--;
				count = m_alShot.Count;
			}
		}
	}

	public void Shot(int nIndex)
	{
		if (nIndex < Gunpoint.Length)
		{
			ShotTraget shotTraget = new ShotTraget();
			shotTraget.Gunpoint = Gunpoint[nIndex];
			shotTraget.Effect = globalVal.Effect.getEffect(ShotEffect);
			shotTraget.Effect.position = shotTraget.Gunpoint.position;
			shotTraget.Effect.forward = base.transform.forward;
			shotTraget.Bullet = globalVal.Effect.getEffect("zd");
			shotTraget.Bullet.position = shotTraget.Gunpoint.position;
			shotTraget.Speed = BulletSpeed;
			Vector3 gunTarget = globalVal.GunTarget;
			gunTarget.y += 2f;
			shotTraget.Target = gunTarget;
			shotTraget.Bullet.forward = shotTraget.Target - shotTraget.Bullet.position;
			shotTraget.ShotTime = Time.realtimeSinceStartup;
			if (m_alShot == null)
			{
				m_alShot = new ArrayList();
			}
			m_alShot.Add(shotTraget);
			shotEffect(shotTraget, true);
		}
	}
}
