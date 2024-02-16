using UnityEngine;

public class GameGoldCtrl : MonoBehaviour
{
	public int m_Score;

	private Transform m_cmTarget;

	private bool m_bPlayEffect;

	private Vector3 m_v3EffectPosition;

	private Vector3 m_v3ScorePoint;

	private Transform m_cmEffect;

	private int m_nSpeed;

	private float m_fAround;

	private int m_nMulitplyingPower;

	public int Score
	{
		get
		{
			return m_Score;
		}
	}

	private void Start()
	{
		GameObject gameObject = GameObject.Find("Main Camera");
		m_nMulitplyingPower = 1;
		if (Vector3.zero == globalVal.GoldPoint)
		{
			GameObject gameObject2 = GameObject.Find("TUI/TUIControl/gametitle/game_gold/0");
			if (null == gameObject2)
			{
				return;
			}
			m_v3ScorePoint = gameObject2.transform.position;
			gameObject2 = GameObject.Find("TUI/TUICamera");
			globalVal.GoldPoint = gameObject2.transform.GetComponent<Camera>().WorldToScreenPoint(m_v3ScorePoint);
			globalVal.GoldPoint.z = 0f;
		}
		m_v3ScorePoint = globalVal.GoldPoint;
	}

	private void Update()
	{
		if (null == m_cmTarget)
		{
			whirl();
		}
		else
		{
			flyToTarget();
		}
		if (m_bPlayEffect)
		{
			playEffect();
		}
	}

	private void whirl()
	{
		m_fAround += 60f * Time.deltaTime;
		base.transform.localEulerAngles = new Vector3(0f, m_fAround, 0f);
	}

	public void setTarget(Transform cmTarget)
	{
		if (!(null != m_cmTarget))
		{
			Transform effect = globalVal.Effect.getEffect("ef_04_add");
			if (null != effect)
			{
				globalVal.Effect.playEffect(effect);
				effect.position = base.transform.GetChild(0).position;
				globalVal.Effect.setAlarm(effect, 0.2f);
			}
			m_v3EffectPosition = globalVal.MainCamera.WorldToScreenPoint(base.transform.position);
			m_cmTarget = cmTarget;
			m_bPlayEffect = false;
			base.transform.position = Vector3.zero;
		}
	}

	private void flyToTarget()
	{
		m_cmTarget = null;
		m_bPlayEffect = true;
		playSound();
		Reset();
		m_nSpeed = 1000;
		globalVal.GameScore += m_Score * globalVal.GoldPower;
		globalVal.AllGold += m_Score * globalVal.GoldPower;
		globalVal.UIScript.refurbishScore();
	}

	private void playEffect()
	{
		if (null == m_cmEffect)
		{
			m_cmEffect = globalVal.Effect.getEffect("ef_26");
			if (null == m_cmEffect)
			{
				Debug.Log("playGoldEffect m_cmEffect = " + m_cmEffect);
				return;
			}
			globalVal.Effect.playEffect(m_cmEffect);
		}
		Vector3 vector = Vector3.Normalize(globalVal.GoldPoint - m_v3EffectPosition);
		vector *= Time.deltaTime * (float)m_nSpeed;
		m_v3EffectPosition += vector;
		m_cmEffect.position = globalVal.MainCamera.ScreenToWorldPoint(m_v3EffectPosition);
		float num = Vector3.Distance(m_v3EffectPosition, m_v3ScorePoint);
		if (m_nSpeed == 10 || 1f >= num)
		{
			m_bPlayEffect = false;
			GameEffect.GetInstance().recycleEffect(m_cmEffect);
			m_cmEffect = null;
			base.transform.gameObject.SetActiveRecursively(false);
		}
	}

	private void playSound()
	{
		GameAudio.GetInstance().playSound("player_get_gold", base.transform.position);
	}

	private void Reset()
	{
		base.transform.position = Vector3.zero;
	}
}
