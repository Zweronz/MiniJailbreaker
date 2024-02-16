using UnityEngine;

public class ActiveEffect : MonoBehaviour
{
	public delegate void OnEffectEvent(ref string eventName);

	public Transform EffectPoint;

	private OnEffectEvent onEffectevent;

	public void Effect(string objName)
	{
		Transform effect = globalVal.Effect.getEffect(objName);
		if (null == effect)
		{
			Debug.Log("not found = " + objName);
		}
		Debug.Log("Effect Point = " + EffectPoint);
		if (null == EffectPoint)
		{
			effect.position = base.transform.position;
			effect.forward = base.transform.forward;
		}
		else
		{
			effect.position = EffectPoint.position;
			effect.eulerAngles = EffectPoint.eulerAngles;
		}
		globalVal.Effect.setAlarm(effect, 2f);
	}

	public void SetEffectEventDelegate(OnEffectEvent onEffectEventDelegate)
	{
		onEffectevent = onEffectEventDelegate;
	}
}
