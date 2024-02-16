using UnityEngine;

public class TerrainWhirl : TerrainTrigger
{
	public float Gravity;

	public Transform BalancePoint;

	public Transform WhirlPoint;

	private void Start()
	{
		Init();
	}

	private void Update()
	{
	}

	public new void Init()
	{
	}

	public new void Trigger(Vector3 v3Point)
	{
		globalVal.VibrateScreen = true;
		Whirl(v3Point);
	}

	public void Whirl(Vector3 v3TriggerPoint)
	{
		if (!(null == BalancePoint) && !(null == WhirlPoint))
		{
			float num = Vector3.Distance(v3TriggerPoint, BalancePoint.position);
			float num2 = Gravity * Time.deltaTime * num * 0.06f;
			Vector3 vector = Vector3.down * num2 + v3TriggerPoint;
			Vector3 vector2 = Vector3.Normalize(BalancePoint.position - v3TriggerPoint);
			Vector3 to = Vector3.Normalize(BalancePoint.position - vector);
			float num3 = Vector3.Angle(-WhirlPoint.up, vector2);
			num3 = ((!(90f >= num3)) ? 1f : (-1f));
			float num4 = Vector3.Angle(vector2, to);
			num4 *= num3;
			Vector3 localEulerAngles = WhirlPoint.localEulerAngles;
			localEulerAngles.x += num4;
			WhirlPoint.localEulerAngles = localEulerAngles;
		}
	}
}
