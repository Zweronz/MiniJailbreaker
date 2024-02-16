using UnityEngine;

public abstract class EffectLife : MonoBehaviour
{
	public float LifecycleTime;

	protected float m_fStartTime;

	protected bool m_bStart;

	private void Start()
	{
		m_bStart = false;
		init();
	}

	private void Update()
	{
		if (!(0f >= LifecycleTime) && m_bStart)
		{
			if (Time.realtimeSinceStartup > m_fStartTime + LifecycleTime)
			{
				m_bStart = false;
				destroy();
			}
			else
			{
				Run();
			}
		}
	}

	public void activation()
	{
		m_fStartTime = Time.realtimeSinceStartup;
		m_bStart = true;
	}

	public abstract void init();

	public abstract void destroy();

	public void Run()
	{
	}
}
