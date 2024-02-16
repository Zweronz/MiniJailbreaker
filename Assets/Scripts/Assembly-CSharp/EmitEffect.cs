using UnityEngine;

public class EmitEffect : EffectLife, EffectTrigger
{
	private void setEmit(Transform cmEmitter, bool bEmit)
	{
		if (null == cmEmitter)
		{
			return;
		}
		ParticleEmitter particleEmitter = cmEmitter.GetComponent(typeof(ParticleEmitter)) as ParticleEmitter;
		if (null != particleEmitter)
		{
			particleEmitter.emit = bEmit;
			return;
		}
		ParticleSystem particleSystem = cmEmitter.GetComponent(typeof(ParticleSystem)) as ParticleSystem;
		if (null != particleSystem)
		{
			if (bEmit)
			{
				particleSystem.Play();
			}
			else
			{
				particleSystem.Stop();
			}
		}
	}

	public void Trigger()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			setEmit(base.transform.GetChild(i), true);
		}
		activation();
	}

	public override void init()
	{
		destroy();
	}

	public override void destroy()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			setEmit(base.transform.GetChild(i), false);
		}
	}
}
