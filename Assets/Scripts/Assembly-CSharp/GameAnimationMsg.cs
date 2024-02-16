using UnityEngine;

public class GameAnimationMsg : MonoBehaviour
{
	public delegate void OnJumpEnd();

	private OnJumpEnd onJumpEnd;

	public void OnJumbEndEvent()
	{
		globalVal.JumpStart = false;
	}

	public void SetEffectEventDelegate(OnJumpEnd onJumpEndDelegate)
	{
		onJumpEnd = onJumpEndDelegate;
	}
}
