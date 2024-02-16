using UnityEngine;

public class TerrainMove : TerrainTrigger
{
	public Transform Target;

	public Vector3 InitPoint;

	public MovePoint[] Points;

	public bool NeedVibrate;

	public float fTime;

	private float fTriggerTime;

	private bool m_bTrigger;

	private int m_nMoveIndex;

	private void Start()
	{
		Init();
	}

	private void FixedUpdate()
	{
		if (null == Target)
		{
			return;
		}
		if (fTime != 0f && Time.realtimeSinceStartup >= fTriggerTime + fTime)
		{
			Init();
		}
		if (m_bTrigger && m_nMoveIndex < Points.Length)
		{
			float num = Vector3.Distance(Target.localPosition, Points[m_nMoveIndex].Point);
			float num2 = Points[m_nMoveIndex].Speed * Time.deltaTime;
			if (num <= num2)
			{
				Target.localPosition = Points[m_nMoveIndex].Point;
				m_nMoveIndex++;
			}
			else
			{
				Vector3 vector = Vector3.Normalize(Points[m_nMoveIndex].Point - Target.localPosition);
				Vector3 vector2 = vector * num2;
				Target.localPosition += vector2;
			}
		}
	}

	public override void Init()
	{
		m_bTrigger = false;
		m_nMoveIndex = 0;
		if (null != Target)
		{
			Target.localPosition = InitPoint;
			Target.gameObject.SetActiveRecursively(true);
		}
	}

	public override void Trigger(Vector3 v3Point)
	{
		Init();
		m_bTrigger = true;
		globalVal.VibrateScreen = NeedVibrate;
		fTriggerTime = Time.realtimeSinceStartup;
		if (null == Target)
		{
			Debug.Log("Name = " + base.transform.parent.name);
			return;
		}
		Target.gameObject.SetActiveRecursively(true);
		playSound();
	}

	public void playSound()
	{
		Transform transform = Target.Find("Audio");
		if (null == transform)
		{
			Debug.Log(string.Concat("transform = ", base.transform, " not found Sound "));
			return;
		}
		ITAudioEvent iTAudioEvent = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			iTAudioEvent = transform.GetChild(i).GetComponent(typeof(ITAudioEvent)) as ITAudioEvent;
			if (null != iTAudioEvent)
			{
				iTAudioEvent.Trigger();
			}
		}
	}
}
