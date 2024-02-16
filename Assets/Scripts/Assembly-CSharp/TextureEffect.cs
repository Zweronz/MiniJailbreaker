using UnityEngine;

public class TextureEffect : EffectLife, EffectTrigger
{
	public int Distance;

	public Transform Plane;

	public int Speed;

	public void Trigger()
	{
		if (!(null == Plane))
		{
			ScaleTexture(0f);
			activation();
		}
	}

	public override void init()
	{
	}

	public override void destroy()
	{
		ScaleTexture(0f);
		base.transform.gameObject.SetActiveRecursively(false);
	}

	private void ScaleTexture(float fScale)
	{
		Plane.localScale = new Vector3(1f, fScale, 1f);
		Plane.GetComponent<Renderer>().material.mainTextureScale = new Vector2(1f, fScale / 10f);
	}

	public new void Run()
	{
		float num = (Time.realtimeSinceStartup - m_fStartTime) * (float)Speed;
		if (num >= (float)Distance)
		{
			num = Distance;
		}
		ScaleTexture(num);
	}
}
