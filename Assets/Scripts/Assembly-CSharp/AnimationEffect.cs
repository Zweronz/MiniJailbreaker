public class AnimationEffect : EffectLife, EffectTrigger
{
	private string c_strAnimation = "Take 001";

	public void Trigger()
	{
		if (!(null == base.transform.GetComponent<UnityEngine.Animation>()[c_strAnimation]))
		{
			base.transform.GetComponent<UnityEngine.Animation>().Play(c_strAnimation);
			activation();
		}
	}

	public override void init()
	{
	}

	public override void destroy()
	{
		base.transform.gameObject.SetActiveRecursively(false);
	}
}
