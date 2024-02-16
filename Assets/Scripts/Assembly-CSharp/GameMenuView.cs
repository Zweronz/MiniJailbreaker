using UnityEngine;

public class GameMenuView : MonoBehaviour
{
	public Transform Player;

	private void Start()
	{
	}

	private void Update()
	{
		if (!(null == Player))
		{
			Player.GetComponent<Animation>().Play("null_run");
		}
	}
}
