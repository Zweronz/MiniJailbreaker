using UnityEngine;

public class Missile : MonoBehaviour
{
	private Vector3 m_v3LastPoint;

	private void Start()
	{
		m_v3LastPoint = base.transform.position;
	}

	private void Update()
	{
		if (globalVal.GameStatus == GAME_STATUS.GAME_RUNNING && !(base.transform.position.z < globalVal.Player.position.z) && !(2.5f < base.transform.position.z - globalVal.Player.position.z) && !(1f < Mathf.Abs(base.transform.position.x - globalVal.Player.position.x)) && !(1.6f < globalVal.PlayerBip.position.y - base.transform.position.y))
		{
			globalVal.GameStatus = GAME_STATUS.GAME_KNOCK;
		}
	}
}
