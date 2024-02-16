using UnityEngine;

public class GameTerrainAction : MonoBehaviour
{
	public Transform TerrainSub;

	public Vector3[] MovePosition;

	public float[] MoveSpeed;

	private int m_nMoveIndex;

	public Vector3[] Rotation;

	public float[] RotationSpeed;

	private int m_nRotationIndex;

	public Transform BalancePoint;

	public float Gravity;

	private bool m_bActive;

	private bool m_bBalance;

	private bool m_bReset;

	private Transform m_cmPlayer;

	private void Start()
	{
		Reset();
	}

	private bool doMove()
	{
		if (m_nMoveIndex >= MovePosition.Length)
		{
			return true;
		}
		float num = Vector3.Distance(TerrainSub.localPosition, MovePosition[m_nMoveIndex]);
		if (0.2f >= num)
		{
			if (m_nMoveIndex + 1 >= MovePosition.Length)
			{
				globalVal.VibrateScreen = false;
				return true;
			}
			m_nMoveIndex++;
			num = Vector3.Distance(TerrainSub.localPosition, MovePosition[m_nMoveIndex]);
		}
		float num2 = MoveSpeed[m_nMoveIndex - 1] * Time.deltaTime;
		if (num <= num2)
		{
			TerrainSub.localPosition = MovePosition[m_nMoveIndex];
			return false;
		}
		Vector3 vector = Vector3.Normalize(MovePosition[m_nMoveIndex] - TerrainSub.localPosition);
		Vector3 vector2 = vector * num2;
		TerrainSub.localPosition += vector2;
		globalVal.VibrateScreen = true;
		return false;
	}

	private bool doRotation()
	{
		if (m_nRotationIndex >= Rotation.Length)
		{
			return true;
		}
		if (TerrainSub.localEulerAngles == Rotation[m_nRotationIndex])
		{
			if (m_nRotationIndex + 1 >= Rotation.Length)
			{
				return true;
			}
			m_nRotationIndex++;
		}
		TerrainSub.localEulerAngles = Vector3.Lerp(TerrainSub.localEulerAngles, Rotation[m_nRotationIndex], Time.deltaTime * RotationSpeed[m_nRotationIndex]);
		return false;
	}

	private void doBalance()
	{
		float num = Vector3.Distance(m_cmPlayer.position, BalancePoint.position);
		float num2 = Gravity * Time.deltaTime * num * 0.1f;
		Vector3 vector = Vector3.down * num2 + m_cmPlayer.position;
		Vector3 vector2 = Vector3.Normalize(BalancePoint.position - m_cmPlayer.position);
		Vector3 to = Vector3.Normalize(BalancePoint.position - vector);
		float num3 = Vector3.Angle(-TerrainSub.up, vector2);
		num3 = ((!(90f >= num3)) ? 1f : (-1f));
		float num4 = Vector3.Angle(vector2, to);
		num4 *= num3;
		Vector3 localEulerAngles = TerrainSub.localEulerAngles;
		localEulerAngles.x += num4;
		TerrainSub.localEulerAngles = localEulerAngles;
		globalVal.VibrateScreen = true;
	}

	private void Update()
	{
		if (m_bActive)
		{
			bool flag = doMove();
			bool flag2 = doRotation();
			if (flag && flag2)
			{
				m_bActive = false;
			}
			m_bReset = true;
		}
		if (m_bBalance)
		{
			doBalance();
			m_bReset = true;
		}
		if (m_bReset && 100f <= Vector3.Distance(m_cmPlayer.position, base.transform.position))
		{
			Reset();
		}
	}

	public void trigger(Transform cmObject)
	{
		m_bActive = true;
		m_cmPlayer = cmObject;
	}

	public void unTrigger()
	{
		m_bActive = false;
		Reset();
	}

	public void Balance(Transform cmObject)
	{
		m_bBalance = true;
		m_cmPlayer = cmObject;
	}

	public void unBalance()
	{
		m_bBalance = false;
		Reset();
		globalVal.VibrateScreen = false;
	}

	public void Reset()
	{
		if (!(null == TerrainSub))
		{
			if (0 < MovePosition.Length)
			{
				TerrainSub.localPosition = MovePosition[0];
			}
			if (0 < Rotation.Length)
			{
				TerrainSub.localEulerAngles = Rotation[0];
			}
			m_nMoveIndex = 1;
			m_nRotationIndex = 0;
			globalVal.VibrateScreen = false;
			m_bReset = false;
		}
	}
}
