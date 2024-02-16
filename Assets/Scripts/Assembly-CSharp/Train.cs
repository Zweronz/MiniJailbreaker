using UnityEngine;

public class Train : MonoBehaviour
{
	private const int c_nDistance = 5;

	public const int c_nSpeed = 40;

	private WayPointNode m_cmNext;

	private bool m_bInit;

	private Vector3 m_v3LastPoint;

	private int m_nCount;

	private void Start()
	{
	}

	private void Update()
	{
		if (!m_bInit)
		{
			return;
		}
		if (null == m_cmNext.WayPoint)
		{
			globalVal.Channel.recycleChannel(base.transform);
			m_bInit = false;
			return;
		}
		float num = Vector3.Distance(base.transform.position, m_cmNext.WayPoint.position);
		if (5f >= num)
		{
			if (5 <= m_nCount)
			{
				globalVal.Channel.recycleChannel(base.transform);
				m_bInit = false;
				return;
			}
			m_cmNext = m_cmNext.PrePoint;
			if (m_cmNext == null || Vector3.zero == m_cmNext.WayPoint.position)
			{
				globalVal.Channel.recycleChannel(base.transform);
				m_bInit = false;
				return;
			}
			m_nCount++;
		}
		Vector3 to = Vector3.Normalize(m_cmNext.WayPoint.position - base.transform.position);
		to = Vector3.Lerp(base.transform.forward, to, Time.deltaTime * 2f);
		base.transform.forward = to;
		m_v3LastPoint = base.transform.position;
		base.transform.position += 40f * Time.deltaTime * base.transform.forward;
		checkPlayerOrEnemy();
	}

	public void init(WayPointNode cmNode)
	{
		m_cmNext = cmNode;
		m_bInit = true;
		base.transform.forward = Vector3.Normalize(m_cmNext.WayPoint.position - base.transform.position);
		m_nCount = 0;
	}

	private void checkPlayerOrEnemy()
	{
		Vector3 v3LastPoint = m_v3LastPoint;
		v3LastPoint.y += 5.3f;
		Vector3 point = v3LastPoint;
		point += base.transform.forward * 13f;
		RaycastHit[] array = Physics.CapsuleCastAll(v3LastPoint, point, 4.5f, base.transform.forward, 40f * Time.deltaTime * 2f, 1 << LayerMask.NameToLayer("Player"));
		GameEnemy gameEnemy = null;
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if ("Charactor" == raycastHit.transform.name)
			{
				if (globalVal.GameStatus == GAME_STATUS.GAME_RUNNING)
				{
					globalVal.GameStatus = GAME_STATUS.GAME_KNOCK;
				}
			}
			else if ("Instance" == raycastHit.transform.name)
			{
				gameEnemy = raycastHit.transform.parent.GetComponent(typeof(GameEnemy)) as GameEnemy;
				if (null != gameEnemy)
				{
					gameEnemy.knock();
				}
				gameEnemy = null;
			}
		}
	}
}
