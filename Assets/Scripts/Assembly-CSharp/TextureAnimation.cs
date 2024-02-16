using UnityEngine;

public class TextureAnimation : MonoBehaviour
{
	public float OffsetX;

	public float OffsetY;

	private void Start()
	{
	}

	private void Update()
	{
		if (!(null == base.transform.GetComponent<Renderer>().material) && !(null == base.transform.GetComponent<Renderer>().material.mainTexture))
		{
			Vector2 zero = Vector2.zero;
			Material[] materials = base.transform.GetComponent<Renderer>().materials;
			foreach (Material material in materials)
			{
				zero = material.mainTextureOffset;
				zero.x += OffsetX * Time.deltaTime;
				zero.x %= 1f;
				zero.y += OffsetY * Time.deltaTime;
				zero.y %= 1f;
				material.mainTextureOffset = zero;
			}
		}
	}
}
