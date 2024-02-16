using UnityEngine;

public class MaterialEffect : MonoBehaviour
{
	public Transform Target;

	public Material[] MaterialList;

	private void Start()
	{
	}

	public void changeMaterial(int nIndex)
	{
		if (!(null == Target) && MaterialList != null && 0 < MaterialList.Length && nIndex < MaterialList.Length)
		{
			Target.GetComponent<Renderer>().material = MaterialList[nIndex];
		}
	}
}
